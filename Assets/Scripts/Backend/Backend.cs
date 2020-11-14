using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SocialPlatforms.Impl;

public static class Backend
{
    [Serializable]
    public struct HighScore
    {
        public string name;
        public int score;
    }

    [Serializable]
    public struct ScoreData
    {
        // The current cumulative score
        public int currentScore;
        // The index of the next unlock
        public int nextUnlock;
        // The cumulative score needed for the next unlock
        public int nextScore;
        // The top 10 scores
        public List<HighScore> top;
    }

    private static readonly string serverAddress = "http://server.lan:2708";
    private static readonly string getStatus = "/status";
    private static readonly string getScoreData = "/scoreData";

    public static bool GetStatus()
    {
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(serverAddress + getStatus);
        HttpWebResponse res = (HttpWebResponse)req.GetResponse();

        StreamReader reader = new StreamReader(res.GetResponseStream());
        string resString = reader.ReadToEnd();
        return resString.Equals("Alive");
    }

    public static ScoreData GetScoreData()
    {
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(serverAddress + getScoreData);
        HttpWebResponse res = (HttpWebResponse)req.GetResponse();

        StreamReader reader = new StreamReader(res.GetResponseStream());
        string resString = reader.ReadToEnd();
        Debug.Log(resString);
        ScoreData data = JsonUtility.FromJson<ScoreData>(resString);
        return data;
    }


}
