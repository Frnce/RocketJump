using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CharacterSelectionManager : MonoBehaviour
{
    public Text startMatch;
    public GameObject[] Players;
    bool AcceptButton_1, AcceptButton_2,EnterButton;

    int numOfPlayers = 0;
    bool hasJoined_P1 = false, hasJoined_P2 = false, hasJoined_P3 = false, hasJoined_P4 = false;
    float direction_P1 = 0, direction_P2 = 0;
    int characterIndex;
    // Use this for initialization
    void Start ()
    {
        startMatch.enabled = false;
        PlayerPrefs.DeleteAll(); // USE WITH CAUTION
        GetInput();
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
                var playerText = Players[0].GetComponentsInChildren<Text>();
                foreach (var item in playerText)
                {
                    if (item.name == "Status 1")
                    {
                        item.text = "Player 1 Has Joined";
                    }
                }
                numOfPlayers++;
                PlayerPrefs.SetInt("NumberOfPlayers", numOfPlayers);
                PlayerPrefs.SetInt("SelectedCharacter_P1", characterIndex);
                Debug.Log("Player One Joined");
                hasJoined_P1 = true;
            }
        }
        if (AcceptButton_2)
        {
            if (!hasJoined_P2)
            {
                var playerText = Players[1].GetComponentsInChildren<Text>();
                foreach (var item in playerText)
                {
                    if (item.name == "Status 2")
                    {
                        item.text = "Player 2 Has Joined";
                    }
                }
                numOfPlayers++;
                PlayerPrefs.SetInt("NumberOfPlayers", numOfPlayers);
                PlayerPrefs.SetInt("SelectedCharacter_P2", characterIndex);
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
        direction_P1 = hInput.GetAxis("Horizontal_P1");
        direction_P2 = hInput.GetAxis("Horizontal_P2");
        EnterButton = hInput.GetButtonDown("Enter");
    }

    void CheckIfReadyToStart()
    {
        if(numOfPlayers > 1)
        {
            startMatch.enabled = true;
        }
    }


    //--Character Select Function--!!
    //public void toggleLeft(GameObject player)
    //{
    //    player.GetComponentInChildren<Image>().sprite = characters[characterIndex].sprite;
    //    characterIndex--;
    //    if (characterIndex < 0)
    //    {
    //        characterIndex = characters.Length - 1;
    //    }
    //    if(characters[characterIndex].isSelected == true)
    //    {
    //        characterIndex--;
    //    }

    //    player.GetComponentInChildren<Image>().sprite = characters[characterIndex].sprite;
    //}
    //public void toggleRight(GameObject player)
    //{
    //    player.GetComponentInChildren<Image>().sprite = characters[characterIndex].sprite;
    //    characterIndex++;
    //    if (characterIndex == characters.Length)
    //    {
    //        characterIndex = 0 ;
    //    }
    //    if (characters[characterIndex].isSelected == true)
    //    {
    //        characterIndex++;
    //    }
    //    player.GetComponentInChildren<Image>().sprite = characters[characterIndex].sprite;
    //}
}
