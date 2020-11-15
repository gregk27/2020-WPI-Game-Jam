using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTowerUIScript : MonoBehaviour
{
    public float fadeInStartTime = 5;
    public float fadeTime = 1;
    private float startTime = 0;
    private CanvasGroup thisGroup;
    private bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        thisGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        //print("test");
        //print(Time.time); 
        //print(fadeInStartTime + startTime);

        //print(started);
        float timeSinceStart = fadeInStartTime + startTime;
        if (Time.time > timeSinceStart && started) {
            gameObject.GetComponent<CanvasGroup>().interactable = true;

            //could do with lerp?
            float interpolateTime = (Time.time - timeSinceStart) / fadeTime;
            thisGroup.alpha = interpolateTime;
        }
    }

    public void StartTimer() {
        started = true;
        startTime = Time.time;
    }
}
