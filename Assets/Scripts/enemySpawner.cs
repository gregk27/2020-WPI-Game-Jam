using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> enemyTypes;

    public int numberOfEnemies = 100;

    public int spawnTimer = 10;
    private int timeToSpawn;

    public float leftX = -100;
    public float rightX = 100;

    public float yMin = -10;
    public float yMax = 10;

    public static EnemySpawner instance;

    //private List<GameObject> enemies;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        timeToSpawn = spawnTimer;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //spawn code
        if(timeToSpawn == 0 && numberOfEnemies > 0) {
            //get random position
            Vector3 randPos = new Vector3(leftX, Random.Range(yMin, yMax), 0);
            //clone
            DuplicateEnemy(randPos);


            //reset spawn timer
            timeToSpawn = spawnTimer;
        }

        timeToSpawn--;
    }

    public void DuplicateEnemy(Vector3 pos) {
        //get random enemy
        GameObject randEnemy = enemyTypes[Random.Range(0, enemyTypes.Count)];
        //clone it
        Instantiate(randEnemy, position: pos, Quaternion.identity);
    }
}
