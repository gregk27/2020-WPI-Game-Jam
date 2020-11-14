using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomizeText : MonoBehaviour {
    // Start is called before the first frame update
    public List<string> texts;

    public static RandomizeText instance;

    private void Start() {
        instance = this;
    }

    public void SetRandomText() {
        gameObject.GetComponent<Text>().text = texts[Random.Range(0, texts.Count)];
    }
}
