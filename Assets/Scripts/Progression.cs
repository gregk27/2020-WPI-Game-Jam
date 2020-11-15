using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Progression
{
    private static readonly string UNLOCK_TURRET = "TOWER";
    private static readonly string UNLOCK_PLAYER = "PLAYER";

    [Serializable]
    public struct Unlock {
        public int baseScore;
        public string name;
        public string desc;
        public float scaleFactor;
        public string type;
        public string id;
        public int value;
    }

    [Serializable]
    public struct ProgressionData
    {
        public List<Unlock> unlocks;
    }

    public struct TurretInfo
    {
        public string id;
        public int upgrades;
    }

    public struct PlayerInfo
    {
        public int jumpCount;
        public int health;
    }

    public static List<Unlock> unlocks { get; private set; }

    public static Dictionary<string, TurretInfo> turrets { get; private set; }
    public static PlayerInfo player { get; private set; }
    public static int nextUnlock { get; private set; }


    static Progression()
    {
        Debug.Log("Initialising");
        TextAsset jsonFile = Resources.Load<TextAsset>("Progression");
        unlocks = JsonUtility.FromJson<ProgressionData>(jsonFile.text).unlocks;
        Update();
        Debug.Log("Done");
    }

    private static void Update()
    {
        turrets = new Dictionary<string, TurretInfo>();
        player = new PlayerInfo();
        
        Backend.ScoreData scoreDat = Backend.GetScoreData();
        nextUnlock = scoreDat.nextUnlock;

        Unlock u;
        for(int i=0; i<scoreDat.nextUnlock; i++)
        {
            u = unlocks[i];
            if(u.type == UNLOCK_TURRET)
            {
                if (!turrets.ContainsKey(u.id))
                {
                    //If the turret is not in the list, create a new turret and add it
                    TurretInfo info;
                    info.id = u.id;
                    info.upgrades = u.value;
                    turrets.Add(u.id, info);
                } else {
                    // If the turret is in the list, update it
                    TurretInfo info = turrets[u.id];
                    info.upgrades += u.value;
                }
            } else if (u.type == UNLOCK_PLAYER) {
                switch (u.id)
                {
                    case "JUMP":
                        player = new PlayerInfo() { jumpCount = player.jumpCount + u.value, health = player.health };
                        break;
                    case "HEALTH":
                        player = new PlayerInfo() { jumpCount = player.jumpCount, health = player.health + u.value };
                        break;
                }
            }
        }
    }

    
}
