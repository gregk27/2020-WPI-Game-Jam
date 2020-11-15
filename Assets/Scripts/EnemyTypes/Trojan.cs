using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trojan : EnemyBase {

    public int spawnCount = 3;
    public Trojan() : base(2, 2) {

    }
    public override void Move() {
        base.Move();
        //if it collides with a projectile
        if(false){
            //EnemySpawner.instance.DuplicateEnemy(transform.position);
        }
        
    }

    public override void Hit() {
        //summon new 
        print(health);
        if (health <= 1) {
            GameObject[] enemies = { EnemySpawner.instance.virus, EnemySpawner.instance.worm, EnemySpawner.instance.spyware };
            for(int i=0; i<spawnCount; i++) {
                //print("inst");
                Instantiate(
                    enemies[Random.Range(0, enemies.Length)], 
                    position: transform.position, 
                    Quaternion.identity, 
                    parent: EnemySpawner.instance.gameObject.transform
                    );
            }
        }
        base.Hit();
    }

}