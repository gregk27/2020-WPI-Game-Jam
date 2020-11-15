using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletBase : MonoBehaviour
{

    Collider2D bulletCollider;

    // Start is called before the first frame update
    void Start()
    {
        bulletCollider = this.GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if()
    }
}
