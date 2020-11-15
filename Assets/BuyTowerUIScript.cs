using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyTowerUIScript : MonoBehaviour
{
    public List<Sprite> towerIcons;
    public Text towerName;
    public Image towerImage;
    public Text towerCost;
    private int thisTower = 0;

    // Start is called before the first frame update
    void Start() {
        towerImage.sprite = towerIcons[thisTower];
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKey(KeyCode.RightArrow)) {
            NextTower();
        }
        if (Input.GetKey(KeyCode.LeftArrow)) {
            PreviousTower();
        }
        
    }

    public void NextTower() {
        thisTower++;
        if(thisTower == towerIcons.Count) {
            thisTower = 0;
        }
        towerImage.sprite = towerIcons[thisTower];
    }

    public void PreviousTower() {
        thisTower--;
        if (thisTower == -1) {
            thisTower = towerIcons.Count - 1;
        }
        towerImage.sprite = towerIcons[thisTower];
    }
}
