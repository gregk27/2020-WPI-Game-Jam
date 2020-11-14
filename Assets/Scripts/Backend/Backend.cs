using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions.Must;
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
    private static readonly string pushScore = "/addScore";

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

    public static void PushScore(string name, int score)
    {
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(serverAddress + pushScore);

        Debug.Log("Creating post");
        string postData = "id=" + Uri.EscapeDataString(getID())
                        + "&name=" + Uri.EscapeDataString(name.Length > 32 ? name.Substring(0, 32) : name)
                        + "&score=" + score;
        Debug.Log(postData);
        byte[] data = Encoding.ASCII.GetBytes(postData);

        req.Method = "POST";
        req.ContentType = "application/x-www-form-urlencoded";
        req.ContentLength = data.Length;

        Debug.Log("Creating stream");
        using (var stream = req.GetRequestStream())
        {
            stream.Write(data, 0, data.Length);
        }

        Debug.Log("Getting response");
        HttpWebResponse res = (HttpWebResponse)req.GetResponse();

        Debug.Log("Request sent");
        StreamReader reader = new StreamReader(res.GetResponseStream());
        string resString = reader.ReadToEnd();
        Debug.Log(resString);
    }

    private static string getID()
    {
        // If the UUID exists, return it
        if (PlayerPrefs.HasKey("id"))
        {
            return PlayerPrefs.GetString("id");
        }

        // Otherwise generate and save
        string key = System.Guid.NewGuid().ToString();
        PlayerPrefs.SetString("id", key);
        return key;
    }
}
