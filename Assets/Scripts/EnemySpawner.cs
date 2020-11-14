using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    //get all the enemies
    public GameObject virus;
    public GameObject worm;
    public GameObject spyware;
    public GameObject trojan;
    public GameObject ransomware;

    public string pathToWavesFile;
    private List<FileInfo> waveFiles;

    //get list of enemies currently on screen
    private List<GameObject> enemies = new List<GameObject>();

    //get ui waves text
    public Text waveText;
    

    //get global multipliers
    public float speedMul = 0.5f;
    public float healthMul = 1;

    //list of waves
    //next list is of rows
    //final list is 

    //stores all waves
    private List<List<List<string>>> waves = new List<List<List<string>>>();

    //stores only current wave
    private List<List<string>> thisWave = new List<List<string>>();

    public int currWave = 0;
    private int waveTime = 0;

    public int spawnTimer = 10;
    private int timeToSpawn;

    public float leftX = -100;
    public float rightX = 100;

    public float yMin = -10;
    public float yMax = 10;

    public static EnemySpawner instance;

    //private List<GameObject> enemies;
    // Start is called before the first frame update

    private static bool IsNotCSV(FileInfo fi) {
        return !(fi.Extension == ".csv");
    }

    void Start()
    {
        //set public static instance of this
        instance = this;

        //set spawntimer
        timeToSpawn = spawnTimer;

        //get waves 
        waveFiles = (new DirectoryInfo(pathToWavesFile).GetFiles()).ToList();

        //remove non .csv files
        waveFiles.RemoveAll(IsNotCSV);

        //sort
        //this function takes two inputs and returns if thing one should go infront of thing 2
        waveFiles.Sort((file1, file2) => {
            //print(file1.Name);
            return int.Parse(Path.GetFileNameWithoutExtension(file1.Name)) - int.Parse(Path.GetFileNameWithoutExtension(file2.Name));
        });

        

        
        foreach (FileInfo fi in waveFiles) {
            //print(fi.Name);
            List<List<string>> thisWave = new List<List<string>>();
            
            //from https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo.opentext?view=net-5.0
            // Open the stream and read it back.
            using (StreamReader sr = fi.OpenText()) {
                string s = "";
                while ((s = sr.ReadLine()) != null) {
                    //s is the line
                    string[] lineEnemies = s.Split(',');
                    thisWave.Add(lineEnemies.ToList());
                }
            }
            waves.Add(thisWave);
            
            //print(waves.Count());
            
        }

        //this array should have all the waves
        //print(waves);

        //set thisWave to be first wave
        thisWave = waves[currWave];
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //spawn code
        if(timeToSpawn == 0) {
            //loop through rows in wave, if wavetime is smaller than rows
            int i = 0;
            foreach(List<string> horizontalRow in thisWave) {
                //this maps the vertical spawn to anywhere between yMin and yMax
                float verticalSpawn = i * (yMax - yMin) / 10 + yMin;
                Vector3 pos = new Vector3(leftX, verticalSpawn, -2);
                //spawn whichever enemy it is

                //TODO: this throws a bug

                //horizontalRow.ForEach(print);
                //print(currWave);
                //print(waveTime);

                //spawn enemy if wavetime is smaller than horizontal row
                if (waveTime < horizontalRow.Count) {
                    GameObject thisEnemy = null;

                    switch (horizontalRow[waveTime]) {
                        case "V":
                            //virus
                            thisEnemy = Instantiate(virus, position: pos, Quaternion.identity);
                            break;
                        case "W":
                            //worm
                            thisEnemy = Instantiate(worm, position: pos, Quaternion.identity);
                            break;
                        case "S":
                            //spyware
                            thisEnemy = Instantiate(spyware, position: pos, Quaternion.identity);
                            break;
                        case "T":
                            //trojan
                            thisEnemy = Instantiate(trojan, position: pos, Quaternion.identity);
                            break;
                        case "R":
                            //ransomware
                            thisEnemy = Instantiate(ransomware, position: pos, Quaternion.identity);
                            break;
                        default:
                            break;
                    }
                    if (thisEnemy != null) {
                        enemies.Add(thisEnemy);
                    }
                }
                //increment loop count
                i++;
            }

            waveTime++;
            //if at end of wave
            if(waveTime >= thisWave[0].Count) {
                //wait until all enemies are ded
                if(enemies.Count == 0) {
                    //increment waves
                    currWave++;
                    waveText.text = (currWave + 1).ToString();

                    //set waveTime to 0
                    waveTime = 0;

                    //load new wave
                    thisWave = waves[currWave];
                }
                
            }

            /*
            //get random position
            Vector3 randPos = new Vector3(leftX, Random.Range(yMin, yMax), -2);
            //clone
            DuplicateEnemy(randPos);
            */

            //reset spawn timer
            timeToSpawn = spawnTimer;
        }

        //go through enemy list
        //if enemy doesn't exist, remove it
        //using for loop for efficiency
        for(int i = 0; i<enemies.Count; i++) {
            if (enemies[i] == null) {
                enemies.RemoveAt(i);
            }
        }

        timeToSpawn--;
    }
}
