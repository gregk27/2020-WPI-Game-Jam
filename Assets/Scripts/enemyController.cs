using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;
using Pathfinding;

public class enemyController : MonoBehaviour
{

    public float speed = 2f;
    public GameObject enemySpawner;

    public float randomMoveChance = 0.1f;
    public float randomMoveDistance = 5f;
    private bool randomlyMoving = false;

    public Tilemap tilemap;

    private float rightX;
    private float topY;
    private float bottomY;
    IAstarAI ai;

    private Vector3 originalEnd = new Vector3();
    // Start is called before the first frame update
    void Start()
    {
        //get ai
        ai = GetComponent<IAstarAI>();

        //set rightX
        enemySpawner spawner = enemySpawner.GetComponent<enemySpawner>();
        rightX = spawner.rightX;
        topY = spawner.yMax;
        bottomY = spawner.yMin;

        //go to the exact right
        originalEnd = new Vector3(rightX, transform.position.y, 0);
        ai.destination = originalEnd;
    }

    private Vector2[] directions = { Vector2.down, Vector2.left, Vector2.right, Vector2.up};
    

    // Update is called once per frame
    void Update()
    {
        /*
        //random path change
        //don't randomly move if currently moving
        if (!randomlyMoving) {
            if (Random.Range(0f, 1f) < randomMoveChance) {
                //get random tile
                Vector3 potentialPos = transform.position +
                new Vector3(Random.Range(0, randomMoveDistance), Random.Range(-randomMoveDistance, randomMoveDistance), 0);

                bool possibleSpot = true;

                //if it's a possible spot
                if (possibleSpot) {
                    //set random pos
                    ai.destination = potentialPos;
                    //the enemy is now randomly moving
                    randomlyMoving = true;
                }

                
                
            }
        }

        //some code from https://arongranberg.com/astar/docs/wander.html
        if (randomlyMoving) {
            //see if overlapping tilemap
            List<Collider2D> results = new List<Collider2D>();
            gameObject.GetComponent<CircleCollider2D>().OverlapCollider(new ContactFilter2D(), results);
            if (results.Contains(tilemap.GetComponent<Collider2D>())) {
                //then reset
                ai.destination = originalEnd;
                ai.SearchPath();
                //enemy is no longer randomly moving
                randomlyMoving = false;
                print("please print");
            }

            //if at the end of the path
            if (!ai.pathPending && (ai.reachedEndOfPath || !ai.hasPath)) {
                ai.destination = originalEnd;
                ai.SearchPath();
                //enemy is no longer randomly moving
                randomlyMoving = false;
            }
            
        }
        */
        
        

        //if it is at the right, then die
        //print(gameObject.transform.position.x);
        if (gameObject.transform.position.x >= rightX) {
            //print("die");
            //kill children
            int childs = transform.childCount;
            for (int i = childs - 1; i > 0; i--) {
                GameObject.Destroy(transform.GetChild(i).gameObject);
            }

            //then destroy object
            Destroy(gameObject);


        }


    }
}
