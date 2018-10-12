using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public float maxScore = 5f;
    float maxCurrentScore;
    Text text;
    [HideInInspector]
    public static string giveScoreTo;
    [HideInInspector]
    public static float currentScore;
    // Use this for initialization
    void Start ()
    {
        text = GetComponent<Text>();
        text.text = "0";
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (giveScoreTo == text.tag)
        {
            text.text = currentScore.ToString();
        }
        if(giveScoreTo == text.tag)
        {
            text.text = currentScore.ToString();
        }
	}

    public static void GiveScore(string GiveScoretoWho)
    {
        currentScore++;
        giveScoreTo = GiveScoretoWho;
    }
}
