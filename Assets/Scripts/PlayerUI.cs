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

    public GameObject tower;
    public GameObject towerParent;
    public GameObject enemyManager;
    public GameObject bulletManager;

    private List<CanvasGroup> uiList = new List<CanvasGroup>();

    private Vector3 startPos;

    //also do particles here, why not
    //should maybe have own script, oh well
    //this could all be better
    private ParticleSystem particleSystem;

    //private PlayerController playerController;
    void HideUI() {
        //hide all the UI's
        foreach(CanvasGroup ui in uiList) {
            ui.interactable = false;
            ui.alpha = 0;
            ui.blocksRaycasts = false;
        }
    }

    static void ShowUI(GameObject g) {
        CanvasGroup c = g.GetComponent<CanvasGroup>();
        c.interactable = true;
        c.alpha = 1;
        c.blocksRaycasts = true;
    }

    void Start()
    {
        uiList.Add(firstTowerUI.GetComponent<CanvasGroup>());
        uiList.Add(buyTowerUI.GetComponent<CanvasGroup>());
        uiList.Add(onTowerUI.GetComponent<CanvasGroup>());
        uiList.Add(mainUI.GetComponent<CanvasGroup>());
        uiList.Add(deathScreenUI.GetComponent<CanvasGroup>());
        uiList.Add(startScreenUI.GetComponent<CanvasGroup>());

        ScoreManager.UpdateUI();

        HideUI();
        ShowUI(startScreenUI);

        particleSystem = GetComponentInChildren<ParticleSystem>();

        //playerController = gameObject.GetComponent<PlayerController>();
        //disable movement
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        //get position
        startPos = transform.position;
    }

    public void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            //HideUI();
            //ShowUI(mainUI);
            //ShowUI(buyTowerUI);
            if (ScoreManager.RemoveScore(100)) {
                //get position
                RaycastHit2D pos = Physics2D.Raycast(transform.position, Vector2.down, distance: Mathf.Infinity, layerMask: LayerMask.GetMask("Obstacle"));
                GameObject turret = Instantiate(tower, pos.point + Vector2.up * 0.5f, Quaternion.identity, parent: towerParent.transform);
                turret.GetComponent<TurretBase>().enemyManager = enemyManager;
                turret.GetComponent<TurretBase>().bulletManager = bulletManager;
            }
            
        }
    }

    //called when game start button is clicked
    public void StartButtonClicked() {
        //burst of particles
        particleSystem.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        //particleSystem.
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
        transform.position = startPos;

        //remove towers
        for (int i = 0; i < towerParent.transform.childCount; i++) {
            GameObject.Destroy(towerParent.transform.GetChild(i).gameObject);
        }

        ScoreManager.Reset();
        ScoreManager.UpdateUI();

        //hide UI
        HideUI();
        ShowUI(mainUI);

        EnemySpawner.instance.ResetEnemies();
        EnemySpawner.instance.gameIsPlaying = true;

        firstTowerUI.GetComponent<FirstTowerUIScript>().StartTimer();
    }

    public void PlayerDied() {
        HideUI();
        ShowUI(deathScreenUI);
        EnemySpawner.instance.gameIsPlaying = false;
        
        particleSystem.Play();
        gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);


        RandomizeText.instance.SetRandomText();
    }
}
