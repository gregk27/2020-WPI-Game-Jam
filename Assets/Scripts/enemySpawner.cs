using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

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

    //list of waves
    //next list is of rows
    //final list is collumns

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
            //loop through rows in wave
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
                switch (horizontalRow[waveTime]) {
                    case "V":
                        //virus
                        Instantiate(virus, position: pos, Quaternion.identity);
                        break;
                    case "W":
                        //worm
                        Instantiate(worm, position: pos, Quaternion.identity);
                        break;
                    case "S":
                        //spyware
                        Instantiate(spyware, position: pos, Quaternion.identity);
                        break;
                    case "T":
                        //trojan
                        Instantiate(trojan, position: pos, Quaternion.identity);
                        break;
                    case "R":
                        //ransomware
                        Instantiate(ransomware, position: pos, Quaternion.identity);
                        break;
                    default:
                        break;
                }

                //increment loop count
                i++;
            }

            waveTime++;
            //if at end of wave
            if(waveTime == thisWave[0].Count) {
                //this does not show wave 2
                currWave++;
                waveTime = 0;
                //throws error
                thisWave = waves[currWave];
                print(currWave);
                //wait until all enemies are ded
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

        timeToSpawn--;
    }

    public void DuplicateRandomEnemy(Vector3 pos) {
        /*
        //get random enemy
        GameObject randEnemy = enemyTypes[Random.Range(0, enemyTypes.Count)];
        //clone it
        Instantiate(randEnemy, position: pos, Quaternion.identity);
        */
        //do this in Trojan.cs
    }
}
