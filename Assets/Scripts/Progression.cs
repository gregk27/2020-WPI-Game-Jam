using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Progression
{
    
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

    public static List<Unlock> unlocks { get; private set; }


    static Progression()
    {
        Debug.Log("Initialising");
        TextAsset jsonFile = Resources.Load<TextAsset>("Progression");
        unlocks = JsonUtility.FromJson<ProgressionData>(jsonFile.text).unlocks;
    }

    
}
