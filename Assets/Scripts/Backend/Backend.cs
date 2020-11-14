using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public static class Backend
{

private static readonly string serverAddress = "http://server.lan:2708";
    private static readonly string getStatus = "/status";

    public static bool GetStatus()
    {
        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(serverAddress + getStatus);
        HttpWebResponse res = (HttpWebResponse)req.GetResponse();

        StreamReader reader = new StreamReader(res.GetResponseStream());
        string resString = reader.ReadToEnd();
        return resString.Equals("Alive");
    }


}
