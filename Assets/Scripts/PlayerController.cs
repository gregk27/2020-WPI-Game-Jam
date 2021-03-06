﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour {
    //from https://www.codegrepper.com/code-examples/csharp/player+controller+script+unity
    Rigidbody2D rigidbody2D;

    public GameObject tilemap;

    public BoxCollider2D groundCollider;

    public float originalSpeed = 2.0f;
    private float speed;
    public float jumpHeight = 8.0f;
    public float glideSpeed = 0.2f;

    public bool glideEnabled = true;

    private Vector3 moveDirection = Vector3.zero;

    private Vector3 startPos;

    
    public int jumpCount = 3;
    public float downGravity = 4;
    public float defaultGravity = 1;
    private int currentJumpCount;

    private int timeSinceLastJump = 0;

    public string spikeName = "";

    //this is the minimum height before the player dies
    public float minHeight = -11;

    public bool isGrounded;
    //this variable tracks if up arrow is pressed
    //the player needs to release up in order to jump again
    bool jumped = false;
    void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentJumpCount = jumpCount;
        speed = originalSpeed;

        startPos = transform.position;
        //particleSystem.
    }

    void jump() {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpHeight);
        //also boost horizontal movement a bit
        //set jumped to true
        //the player cannot jump again until they release up
        timeSinceLastJump = 0;
        jumped = true;
    }

    void Die() {
        //print("die");

        //set rigidbody to static
        rigidbody2D.bodyType = RigidbodyType2D.Static;
        GetComponent<PlayerUI>().PlayerDied();
    }

    void FixedUpdate() {
        //jump
        //from https://answers.unity.com/questions/1384169/how-do-i-check-if-my-player-is-grounded-with-physi.html
        
        //jump code
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            //if the player has not jumped (the up arrow was last down)
            if (!jumped) {
                //if the player has jumps left, decrement jump counter and jump
                if (currentJumpCount > 0) {
                    currentJumpCount--;
                    jump();
                    //print(currentJumpCount);
                }
                
            }
            
        } else {
            jumped = false;
        }
        //the "timeSinceLastJump>5" is just to make sure that the player doesnt get an extra jump
        //reset jump count
        if (isGrounded && timeSinceLastJump>5) {
            currentJumpCount = jumpCount;
            //print(isGrounded);
        }

        //horizontal movement code
        float horizontalAxis = Input.GetAxis("Horizontal");
        float horizontalMovement;

        //grounded movement code
        if (horizontalAxis > 0) {
            horizontalMovement = (float)Math.Sqrt(horizontalAxis);
        } else {
            horizontalMovement = (float)-Math.Sqrt(-horizontalAxis);
        }
        horizontalMovement = horizontalMovement * speed;
        rigidbody2D.velocity = new Vector2(horizontalMovement, rigidbody2D.velocity.y);


        //print(isGrounded);

        //downwards movement code
        if (Input.GetAxis("Vertical") < 0) {
            rigidbody2D.gravityScale = downGravity;
        } else {
            rigidbody2D.gravityScale = defaultGravity;
        }

        //glide code
        //change key
        if (glideEnabled) {
            if (Input.GetKey(KeyCode.Space)) {
                if (rigidbody2D.velocity.y < -glideSpeed) {
                    rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, -glideSpeed);
                }
            }
        }

        //if less than height, also die
        if (transform.position.y < minHeight) { 
            Die();
            transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
        }

        //if moving left or right, flip character
        if(rigidbody2D.velocity.x > 0) {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        } else {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        

        //step time since last jump
        timeSinceLastJump++;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (groundCollider.IsTouching(col.collider)) {
            //print("touching");
            isGrounded = true;
        }

        //spike code
        ContactPoint2D[] contacts = new ContactPoint2D[10];

        if (col.gameObject == tilemap) {
            //from https://answers.unity.com/questions/1728724/destroy-tiles-that-collide-with-object.html
            //get contacts
            int contactCount = col.contactCount;
            if (contactCount > contacts.Length)
                contacts = new ContactPoint2D[contactCount];
            col.GetContacts(contacts);

            //get colliding tilemap
            Tilemap tilemap = col.gameObject.GetComponent<Tilemap>();

            Vector3 hitPosition = Vector3.zero;
            //loop through contacts
            for (int i = 0; i != contactCount; ++i) {

                hitPosition.x = contacts[i].point.x;
                hitPosition.y = contacts[i].point.y;
                
                //get tile at contact point
                TileBase tile = tilemap.GetTile(tilemap.WorldToCell(hitPosition) + Vector3Int.down);
                try {
                    print(tile.name);
                    if (tile.name == spikeName) {
                        //die
                        Die();
                    }
                }catch(NullReferenceException e) {
                    return;
                } 
            }
        }

        
        //print(col.collider);
        //print(col.otherCollider);
    }

    void OnCollisionExit2D(Collision2D col) {
        //print(col.gameObject);
        //print(tilemap);
        if(col.gameObject == tilemap) {
            isGrounded = false;
            //print(isGrounded);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D col) {
        //enemy colision 
        if(col.gameObject.tag == "Enemy") {
            Die();
        }
    }
}
