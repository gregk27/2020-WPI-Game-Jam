using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;


public class EnemyBase : MonoBehaviour
{
    public float speed;
    public float health;

    private Tilemap avoidableObjects;

    private float diagionalPathWeight = 1.4f;

    //variables for getting right side of screen
    //TODO: remove this public variable
    //private GameObject enemySpawner;
    private float rightX;

    private Vector3Int[] directions = {
        Vector3Int.up,
        Vector3Int.up + Vector3Int.right,
        Vector3Int.right,
        Vector3Int.down + Vector3Int.right,
        Vector3Int.down,
        Vector3Int.down + Vector3Int.left,
        Vector3Int.left,
        Vector3Int.up + Vector3Int.left
    };

    //TODO? do with a seperate class to make this all much cleaner, so that there would be no need for all these dictionaries
    //f value is sum of G value and H value
    Dictionary<Vector3Int, float> tileFValues = new Dictionary<Vector3Int, float>();
    Dictionary<Vector3Int, float> tileGValues = new Dictionary<Vector3Int, float>();

    //store parent tile of tile
    Dictionary<Vector3Int, Vector3Int> parentTiles = new Dictionary<Vector3Int, Vector3Int>();

    //list of checked tiles and unchecked tiles
    List<Vector3Int> checkedTiles = new List<Vector3Int>();
    List<Vector3Int> uncheckedTiles = new List<Vector3Int>();

    //this gets H cost in tile space
    float GetHCost(Vector3Int pos, Vector3Int endPos) {
        Vector3Int difference = pos - endPos;
        int largeAxis = Mathf.Max(difference.x, difference.y);
        int smallAxis = Mathf.Min(difference.x, difference.y);
        int remainingTiles = largeAxis - smallAxis;
        return smallAxis * diagionalPathWeight + remainingTiles;
    }
    
    //this takes arguments in tile space
    void FindPath(Vector3Int startTile, Vector3Int targetTile) {
        //add start tile to checked tiles
        
        checkedTiles.Add(startTile);
        tileGValues.Add(startTile, 0);
        tileFValues.Add(startTile, GetHCost(startTile, targetTile));

        Vector3Int currentTile = startTile;

        //loop
        
        while (true) {
            
            //if current tile is end tile, end
            if(currentTile == targetTile) {
                //save path somehow
                break;
            }

            //remove tile from unchecked, and add to checked
            uncheckedTiles.Remove(currentTile);
            checkedTiles.Add(currentTile);

            //loop through directions
            foreach (Vector3Int v in directions) {
                Vector3Int checkingTile = currentTile + v;
                float thisG;
                if(v.magnitude == 1) {
                    //straight 
                    thisG = 1 + tileGValues[currentTile];

                } else {
                    //diagional
                    thisG = diagionalPathWeight + tileGValues[currentTile];
                }
                //add to dictionaries
                float thisF = GetHCost(checkingTile, targetTile) + thisG;
                //if this tile has not previously been counted, or the new values are better
                if (!uncheckedTiles.Contains(checkingTile) || tileFValues[checkingTile] > thisF) {
                    tileGValues.Add(checkingTile, thisG);
                    tileFValues.Add(checkingTile, thisF);
                }

                //if tile not in unchecked tiles, add to unchecked tiles
                //yeah this needs to be done with classes, this is real messy
                if (!uncheckedTiles.Contains(checkingTile)) {
                    uncheckedTiles.Add(checkingTile);
                }
                
            }

            //get tile with smallest F value
            Vector3Int smallestFTile = uncheckedTiles[0];
            float smallestF = tileFValues[smallestFTile];
            foreach (Vector3Int v in uncheckedTiles) {
                if(tileFValues[v] < smallestF) {
                    smallestFTile = v;
                    smallestF = tileFValues[smallestFTile];
                }
            }

            //set the current tile to the smallest F tile
            currentTile = smallestFTile;
        }
    }

    //override
    void FindPath(Vector3 startPos, Vector3 targetPos) {
        Vector3Int startTile = avoidableObjects.WorldToCell(startPos);
        Vector3Int targetTile = avoidableObjects.WorldToCell(targetPos);
        FindPath(startTile, targetTile);
    }

    public virtual void Start() {

        avoidableObjects = GameObject.Find("Walls").GetComponent<Tilemap>();
        //EnemySpawner spawner = enemySpawner.GetComponent<EnemySpawner>();
        rightX = EnemySpawner.instance.rightX;

    }

    public EnemyBase(float speed, float health) {
        this.speed = speed;
        this.health = health;
    }

    void Update() {
        Move();

        //if too far right
        if (gameObject.transform.position.x >= rightX) {
            //print("die");
            //kill children
            int childs = transform.childCount;
            for (int i = childs - 1; i > 0; i--) {
                GameObject.Destroy(transform.GetChild(i).gameObject);
            }

            //then destroy object
            Destroy(gameObject);

        }


    }

    public virtual void Move() {
        //generic move

        //move right
        transform.position += Vector3.right * speed * Time.deltaTime;

    }

}