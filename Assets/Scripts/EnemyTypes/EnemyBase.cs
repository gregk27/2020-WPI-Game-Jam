using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;


public class EnemyBase : MonoBehaviour
{
    public float speed;
    public float health;
    public int currencyGain = 5;

    private int enemyClumpAvoidance = 5;
    private CircleCollider2D collider;

    protected IAstarAI ai;

    private GameObject particleSystem;

    //variables for getting right side of screen
    //TODO: remove this public variable
    //private GameObject enemySpawner;
    private float rightX;

    public virtual void Start() {
        //EnemySpawner spawner = enemySpawner.GetComponent<EnemySpawner>();
        particleSystem = Resources.Load("Enemy Particles") as GameObject;

        collider = GetComponent<CircleCollider2D>();
        rightX = EnemySpawner.instance.rightX;
        ai = GetComponent<IAstarAI>();
        ai.destination = new Vector3(rightX + 10, transform.position.y, transform.position.z);
        ai.maxSpeed = speed;

        //set opacity
        gameObject.GetComponentInChildren<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
    }

    public EnemyBase(float speed, float health) {
        this.speed = speed;
        this.health = health;
    }

    
    void Update() {
        Move();
        
        //if too far right
        if (gameObject.transform.position.x >= rightX) {
            //if done path
            //if (ai.reachedEndOfPath) { 
            //print("die");
            //kill children
            Remove();

        }

        //increase opacity
        gameObject.GetComponentInChildren<SpriteRenderer>().color += new Color(0,0,0,0.01f);


    }

    public void Remove() {
        int childs = transform.childCount;
        for (int i = childs - 1; i > 0; i--) {
            GameObject.Destroy(transform.GetChild(i).gameObject);
        }

        //then destroy object
        Destroy(gameObject);
    }

    private int movingOut;
    private Vector3 moveAway;
    public virtual void Move() {
        //generic move
        //avoid enemy clumping
        RaycastHit2D[] raycast = Physics2D.RaycastAll(transform.position, Vector2.up, 0);
        if (raycast.GetLength(0) > 1) {
            foreach (RaycastHit2D singleHit in raycast) {
                GameObject go = singleHit.transform.gameObject;
                //if enemy
                if (go.CompareTag("Enemy")) {
                    //get difference between objects
                    moveAway = Vector3.Normalize(transform.position - go.transform.position) * Time.deltaTime * speed;
                    movingOut = enemyClumpAvoidance;
                }
            }
        }

        if (movingOut > 0) {
            transform.position += moveAway;
            movingOut--;
        }
        //move right
        //transform.position += Vector3.right * speed * Time.deltaTime;

    }

    //bullets call this function when enemies get hit
    public virtual void Hit() {
        health--;
        //print("hit");
        if (health <= 0){
            //give player score
            ScoreManager.AddScore(currencyGain);

            Instantiate(particleSystem, position:transform.position, rotation:Quaternion.identity);
            Remove();
        }
    }

}