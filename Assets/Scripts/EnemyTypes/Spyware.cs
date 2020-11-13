using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Spyware : EnemyBase {

    private GameObject player;
    
    public int stealCoinRate = 10;

    private int stealCoinTime;
    //while on screen, it steals coins
    public Spyware() : base(2, 2) {
        //follows player
        stealCoinTime = stealCoinRate;
    }

    public override void Start() {
        base.Start();
        player = GameObject.Find("Player");
    }

    public override void Move() {
        ai.destination = player.transform.position;

        //Vector3 movement = Vector3.Normalize(player.transform.position - transform.position);
        //transform.position += movement * speed * Time.deltaTime;

        if (stealCoinTime == 0) {
            //TODO: take coin
            stealCoinTime = stealCoinRate;
        } else {
            stealCoinTime--;
        }
    }
}
