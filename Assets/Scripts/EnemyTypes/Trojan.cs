using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trojan : EnemyBase {
    public Trojan() : base(2, 2) {

    }
    public override void Move() {
        base.Move();
        //if it collides with a projectile
        if(false){
            EnemySpawner.instance.DuplicateEnemy(transform.position);
        }
        
    }


}