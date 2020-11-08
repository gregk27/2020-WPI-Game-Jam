using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject enemy;

    public int numberOfEnemies = 100;

    public int spawnTimer = 10;
    private int timeToSpawn;

    public float leftX = -100;
    public float rightX = 100;

    public float yMin = -10;
    public float yMax = 10;

    private List<GameObject> enemies;
    // Start is called before the first frame update
    void Start()
    {
        timeToSpawn = spawnTimer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //spawn code
        if(timeToSpawn == 0 && numberOfEnemies > 0) {
            Vector3 randPos = new Vector3(leftX, Random.Range(yMin, yMax), 0);
            Instantiate(enemy, position: randPos, Quaternion.identity);
            //reset spawn timer
            timeToSpawn = spawnTimer;
        }

        /*
        print(enemies);

        
        foreach(GameObject e in enemies) {
            e.gameObject.transform.position = e.gameObject.transform.position + Vector3.right * speed;
        }
        */
        timeToSpawn--;
    }
}
