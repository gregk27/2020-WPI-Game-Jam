using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ScoreManager
{
    // Start is called before the first frame update
    private static int score = 500;
    private static int totalScore = 500;
    
    public static int AddScore(int v, bool addTotal = true) {
        score += v;
        if (addTotal) {
            totalScore += v;
        }
        
        UpdateUI();
        return score;
    }

    public static bool RemoveScore(int v) {
        if(score > v) {
            score -= v;
            UpdateUI();
            Debug.Log(score);
            return true;
        }
        
        return false;
    }

    public static void UpdateUI() {
        GameObject mainUI = GameObject.Find("Main UI");

        foreach(Transform child in mainUI.transform) {
            Text text = child.GetComponent<Text>();
            //idk why the score variables need to be reversed
            switch (child.name) {
                case "Score":
                    text.text = totalScore.ToString();
                    break;
                case "Total Score":
                    text.text = score.ToString();
                    break;

                default:
                    break;
            }
        }
    }

    public static void Reset() {
        score = 500;
        totalScore = 500;
    }
}
