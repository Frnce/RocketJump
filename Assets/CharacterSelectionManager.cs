using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectionManager : MonoBehaviour
{
    public Text startMatch;
    public GameObject Player_1;
    public GameObject Player_2;
    bool AcceptButton_1, AcceptButton_2,EnterButton;

    int numOfPlayers = 0;
    bool hasJoined_P1 = false, hasJoined_P2 = false, hasJoined_P3 = false, hasJoined_P4 = false;
    // Use this for initialization
    void Start ()
    {
        startMatch.enabled = false;
        PlayerPrefs.DeleteAll(); // USE WITH CAUTION
        GetInput();
        Player_1.GetComponentInChildren<Image>().color = new Color(0, 0, 0);
        Player_2.GetComponentInChildren<Image>().color = new Color(0, 0, 0);
    }
	
	// Update is called once per frame
	void Update ()
    {
        GetInput();
        CheckIfReadyToStart();
        if (AcceptButton_1)
        {
            if (!hasJoined_P1)
            {
                Player_1.GetComponentInChildren<Image>().color = new Color(0, 255, 0);
                var playerText = Player_1.GetComponentsInChildren<Text>();
                foreach (var item in playerText)
                {
                    if (item.name == "Status 1")
                    {
                        item.text = "Player 1 Has Joined";
                    }
                }
                numOfPlayers++;
                PlayerPrefs.SetInt("NumberOfPlayers", numOfPlayers);
                Debug.Log("Player One Joined");
                hasJoined_P1 = true;
            }
        }
        if (AcceptButton_2)
        {
            if (!hasJoined_P2)
            {
                Player_2.GetComponentInChildren<Image>().color = new Color(255, 0, 0);
                var playerText = Player_2.GetComponentsInChildren<Text>();
                foreach (var item in playerText)
                {
                    if (item.name == "Status 2")
                    {
                        item.text = "Player 2 Has Joined";
                    }
                }
                numOfPlayers++;
                PlayerPrefs.SetInt("NumberOfPlayers", numOfPlayers);
                Debug.Log("Player Two Joined");
                hasJoined_P2 = true;
            }
        }
        if (EnterButton)
        {
            SceneManager.LoadScene("BattleGround");
        }
	}

    void GetInput()
    {
        AcceptButton_1 = hInput.GetButtonDown("Accept_P1");
        AcceptButton_2 = hInput.GetButtonDown("Accept_P2");

        EnterButton = hInput.GetButtonDown("Enter");
    }

    void CheckIfReadyToStart()
    {
        if(numOfPlayers > 1)
        {
            startMatch.enabled = true;
        }
    }
}
