using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mainUI;
    public GameObject firstTowerUI;
    public GameObject buyTowerUI;
    public GameObject onTowerUI;

    void Start()
    {
        //hide all the UI's
        firstTowerUI.SetActive(false);
        buyTowerUI.SetActive(false);
        onTowerUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
