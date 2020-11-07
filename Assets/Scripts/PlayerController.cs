using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //from https://www.codegrepper.com/code-examples/csharp/player+controller+script+unity
    Rigidbody2D rigidbody2D;

    public BoxCollider2D groundCollider;

    public float originalSpeed = 2.0f;
    private float speed;
    public float inAirDrag = 3.0f;
    public float jumpSpeed = 8.0f;
    public float jumpSpeedBoost = 1.5f;

    private Vector3 moveDirection = Vector3.zero;

    public int jumpCount = 3;
    public int downGravity = 4;
    public int defaultGravity = 1;
    private int currentJumpCount;


    bool isGrounded;
    //this variable tracks if up arrow is pressed
    //the player needs to release up in order to jump again
    bool jumped = false;
    void Start() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentJumpCount = jumpCount;
        speed = originalSpeed;
    }

    void jump() {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpSpeed);
        //also boost horizontal movement a bit
        speed = speed * jumpSpeedBoost;
        //set jumped to true
        //the player cannot jump again until they release up
        jumped = true;
    }

    void FixedUpdate() {
        //jump
        //from https://answers.unity.com/questions/1384169/how-do-i-check-if-my-player-is-grounded-with-physi.html
        
        //jump code
        if (Input.GetKey(KeyCode.UpArrow)) {
            if (!jumped) {
                if (isGrounded) {
                    jump();
                    currentJumpCount = jumpCount;
                } else if (currentJumpCount > 1) {
                    currentJumpCount--;
                    jump();
                }
            }
        } else {
            jumped = false;
        }

        //horizontal movement code
        float horizontalAxis = Input.GetAxis("Horizontal");
        float horizontalMovement = 0;
        if (isGrounded) {
            //grounded movement code
            if (horizontalAxis > 0) {
                horizontalMovement = (float)Math.Sqrt(horizontalAxis);
            } else {
                horizontalMovement = (float)-Math.Sqrt(-horizontalAxis);
            }
            horizontalMovement = horizontalMovement * speed;
            rigidbody2D.velocity = new Vector2(horizontalMovement, rigidbody2D.velocity.y);

        } else {
            //in air movement
            horizontalMovement = rigidbody2D.velocity.x + horizontalAxis;
            horizontalMovement = horizontalMovement / inAirDrag;
            horizontalMovement = horizontalMovement * speed;
            rigidbody2D.velocity = new Vector2(horizontalMovement, rigidbody2D.velocity.y);
        }

        print(isGrounded);

        //downwards movement code
        if (Input.GetAxis("Vertical") < 0) {
            rigidbody2D.gravityScale = downGravity;
        } else {
            rigidbody2D.gravityScale = defaultGravity;
        }

        speed = originalSpeed;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (groundCollider.IsTouching(col.collider)) {
            isGrounded = true;
        }
        //print(col.collider);
        //print(col.otherCollider);
    }

    void OnCollisionExit2D(Collision2D col) {
        if(col.gameObject.name == "Tilemap") {
            isGrounded = false;
        }
        
    }
}
