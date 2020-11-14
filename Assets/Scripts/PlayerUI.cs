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
    public GameObject deathScreenUI;
    public GameObject startScreenUI;

    private Vector3 startPos;

    //private PlayerController playerController;
    void HideUI() {
        //hide all the UI's
        firstTowerUI.SetActive(false);
        buyTowerUI.SetActive(false);
        onTowerUI.SetActive(false);
        mainUI.SetActive(false);
        deathScreenUI.SetActive(false);
        startScreenUI.SetActive(false);
    }

    void Start()
    {
        HideUI();
        startScreenUI.SetActive(true);

        //playerController = gameObject.GetComponent<PlayerController>();
        //disable movement
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        //get position
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //called when game start button is clicked
    public void StartButtonClicked() {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        transform.position = startPos;
        HideUI();
        mainUI.SetActive(true);
        EnemySpawner.instance.ResetEnemies();
        EnemySpawner.instance.gameIsPlaying = true;
    }

    public void PlayerDied() {
        HideUI();
        deathScreenUI.SetActive(true);
        EnemySpawner.instance.gameIsPlaying = false;
    }
}
