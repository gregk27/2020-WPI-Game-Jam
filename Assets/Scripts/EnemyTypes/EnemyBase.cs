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

    private float enemyClumpAvoidance = 5;

    protected IAstarAI ai;


    //variables for getting right side of screen
    //TODO: remove this public variable
    //private GameObject enemySpawner;
    private float rightX;

    public virtual void Start() {
        //EnemySpawner spawner = enemySpawner.GetComponent<EnemySpawner>();
        rightX = EnemySpawner.instance.rightX;
        ai = GetComponent<IAstarAI>();
        ai.destination = new Vector3(rightX, transform.position.y, transform.position.z);
        ai.maxSpeed = speed;
    }

    public EnemyBase(float speed, float health) {
        this.speed = speed;
        this.health = health;
    }

    void Update() {
        Move();

        //if too far right
        //if (gameObject.transform.position.x >= rightX) {
        //if done path
        if (ai.reachedEndOfPath) { 
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

    public virtual void Move() {
        //generic move
        //how to avoid enemy clumping

        //move right
        //transform.position += Vector3.right * speed * Time.deltaTime;

    }

}