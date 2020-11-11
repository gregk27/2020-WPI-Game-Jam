using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float speed;
    public float health;


    //variables for getting right side of screen
    public GameObject enemySpawner;
    private float rightX;

    void Start() {
        EnemySpawner spawner = enemySpawner.GetComponent<EnemySpawner>();
        rightX = spawner.rightX;

    }

    public EnemyBase(float speed, float health) {
        this.speed = speed;
        this.health = health;
    }

    void Update() {
        Move();

        //if too far right
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

    public virtual void Move() {
        //generic move
        //move right
        transform.position += Vector3.right * speed * Time.deltaTime;

    }

}