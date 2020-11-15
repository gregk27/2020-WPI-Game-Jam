using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnTowerUIScript : MonoBehaviour
{
    public float fadeTime;
    private CanvasGroup thisGroup;
    private float startTime;
    private bool fadeState = false;


    // Start is called before the first frame update
    void Start()
    {
        thisGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {

        float timeSinceStart = Time.time - startTime;
        gameObject.GetComponent<CanvasGroup>().interactable = fadeState;

        //could do with lerp?
        float interpolateTime = (Time.time - timeSinceStart) / fadeTime;
        if (!fadeState) {
            interpolateTime = 1 - interpolateTime;
        }
        thisGroup.alpha = interpolateTime;
    }

    public void Fade(bool state) {
        fadeState = state;
        startTime = Time.time;
    }

}
