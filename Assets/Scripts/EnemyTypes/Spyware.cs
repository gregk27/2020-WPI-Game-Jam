using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spyware : EnemyBase {

    public GameObject player;
    public int stealCoinRate = 10;

    private int stealCoinTime;
    //while on screen, it steals coins
    public Spyware() : base(2, 2) {
        //follows player
        stealCoinTime = stealCoinRate;
    }

    public override void Move() {
        Vector3 movement = Vector3.Normalize(player.transform.position - transform.position);
        transform.position += movement * speed * Time.deltaTime;

        if(stealCoinTime == 0) {
            //TODO: take coin
            stealCoinTime = stealCoinRate;
        } else {
            stealCoinTime--;
        }
    }
}
