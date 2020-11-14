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

    //also do particles here, why not
    //should maybe have own script, oh well
    //this could all be better
    private ParticleSystem particleSystem;

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

        particleSystem = GetComponentInChildren<ParticleSystem>();

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
        //burst of particles
        particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        //particleSystem.
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
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
        
        particleSystem.Play();
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);

        RandomizeText.instance.SetRandomText();
    }
}
