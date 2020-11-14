using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackendTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Connected: "+Backend.GetStatus());
        Backend.PushScore("Testing", 500);
        Backend.ScoreData data = Backend.GetScoreData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
