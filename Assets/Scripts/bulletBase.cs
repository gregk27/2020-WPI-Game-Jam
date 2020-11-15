using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBase : MonoBehaviour
{
    EnemyBase targetEnemyScript;
    Rigidbody2D rb2d = new Rigidbody2D();
    public int speed;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = transform.up * speed;
        Debug.Log(transform.forward);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            targetEnemyScript = other.gameObject.GetComponent<EnemyBase>();
            targetEnemyScript.health--;
            GameObject.Destroy(gameObject);
        }
        else
        {
            GameObject.Destroy(gameObject);
        }
    }

}
