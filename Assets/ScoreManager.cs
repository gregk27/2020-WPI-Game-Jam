using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ScoreManager
{
    // Start is called before the first frame update
    private static int score = 200;
    private static int totalScore = 200;
    
    public static int AddScore(int v) {
        score += v;
        totalScore += v;
        UpdateUI();
        return score;
    }

    public static int RemoveScore(int v) {
        score -= v;
        UpdateUI();
        return score;
    }

    public static void UpdateUI() {
        GameObject mainUI = GameObject.Find("Main UI");

        foreach(Transform child in mainUI.transform) {
            Text text = child.GetComponent<Text>();
            switch (child.name) {
                case "Score":
                    text.text = score.ToString();
                    break;
                case "Total Score":
                    text.text = totalScore.ToString();
                    break;

                default:
                    break;
            }
        }
    }
}
