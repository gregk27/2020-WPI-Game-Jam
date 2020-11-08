using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{

    public float speed = 2f;
    public GameObject enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //it's a clone
        if (gameObject.name.Contains("(Clone)")){
            gameObject.transform.position += Vector3.right * speed;
            //if far enough right
            if (gameObject.transform.position.x > enemySpawner.GetComponent<enemySpawner>().rightX) {
                Destroy(gameObject);
            }
        }
        
    }
}
