using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : EnemyBase
{
    public float duplicateChance = 0.1f;
    
    public int moveTime = 20;
    private int verticalMovingTime = 0;
    private Vector2 verticalDirection = Vector2.down;

    public Worm(): base(2, 2){}
    
    public override void Move() {
        //if not moving vertically
        if(verticalMovingTime == 0) {
            base.Move();

            //duplicate
            //should probably just duplicate when it gets hit
            //or duplicates once it gets some distance into the level
            if (Random.Range(0f, 1f) < duplicateChance) {
                //duplicate self
                GameObject duplicatedObj = Instantiate(gameObject, position: transform.position, Quaternion.identity);
                Worm otherWorm = duplicatedObj.GetComponent<Worm>();
                otherWorm.verticalMovingTime = moveTime;
                otherWorm.verticalDirection = this.verticalDirection * -1;

                verticalMovingTime = moveTime;
                //in an ideal world
                //move down for n time
                //then continue moving right
            }

        } else {
            //decrement move time
            verticalMovingTime--;
            transform.position += (Vector3) verticalDirection * speed * Time.deltaTime;
        }
        

        
    }
    
}
