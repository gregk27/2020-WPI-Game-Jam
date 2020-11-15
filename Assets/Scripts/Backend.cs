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

/**
 * Class with functions to interface with the backend server
 */
public static class Backend
{
    /**
     * <summary>Structure representing an entry in the top scores list</summary>
     */
    [Serializable]
    public struct HighScore
    {
        /** <summary>Name attached to entry</summary>*/
        public string name;
        /** <summary>Score of entry</summary>*/
        public int score;
    }

    /**
     * <summary>Structure used to represent overall score data</summary>
     */
    [Serializable]
    public struct ScoreData
    {
        /** <summary>The current cumulative score</summary>*/
        public int currentScore;
        /** <summary>The index of the next unlock</summary>*/
        public int nextUnlock;
        /** <summary>The cumulative score needed for the next unlock</summary>*/
        public int nextScore;
        /** <summary>The top 10 scores</summary>*/
        public List<HighScore> top;
    }

    /** <summary>Address of highscore server</summary> */
    private static readonly string serverAddress = "http://192.168.0.4:2708";
    /** <summary>Endpoint to get status</summary> */
    private static readonly string getStatus = "/status";
    /** <summary>Endpoint to get score data</summary> */
    private static readonly string getScoreData = "/scoreData";
    /** <summary>Endpoint to add score data</summary> */
    private static readonly string pushScore = "/addScore";

    /**
     * <summary>Function to get status of server connection<br/>
     * Returns: <c>true</c> if server responds "Alive"</summary>
     */
    public static bool GetStatus()
    {
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(serverAddress + getStatus);
        HttpWebResponse res = (HttpWebResponse)req.GetResponse();

        StreamReader reader = new StreamReader(res.GetResponseStream());
        string resString = reader.ReadToEnd();
        return resString.Equals("Alive");
    }

    /**
     * <summary>Get the overall score data from the server<br/>
     * Returns: ScoreData with response
     * </summary>
     */
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

    /**
     * <summary>Push a score to the server<br/>
     *  - name: Username to use<br/>
     *  - score: Score to be added<br/>
     * </summary>
     */
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

    /**
     * <summary>Get the GUID associated with the user<br/>
     * Checks sharedPrefs, and if not found, generates a new one</summary>
     */
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
