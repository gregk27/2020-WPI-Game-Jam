using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackendTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Backend.GetStatus();
        Backend.ScoreData data = Backend.GetScoreData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
