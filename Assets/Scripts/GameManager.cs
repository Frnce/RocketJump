using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int numberOfRounds = 5;
    public float startDelay = 3f;
    public float endDelay = 3f;
    public Text messageText;
    public GameObject playerPrefab;
    public PlayerManager[] players;

    private int roundNumber;
    private WaitForSeconds startWait;
    private WaitForSeconds endWait;
    private PlayerManager roundWinner;
    private PlayerManager gameWinner;
	// Use this for initialization
	void Start ()
    {
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);

        SpawnAllPlayer();

        StartCoroutine(GameLoop());
	}
    private void SpawnAllPlayer()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].playerInstance = Instantiate(playerPrefab, players[i].spawnPoint.position, players[i].spawnPoint.rotation) as GameObject;
            players[i].playerNumber = i + 1;
            players[i].Setup();
        }
    }
    private IEnumerator GameLoop()
    {
        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if(gameWinner != null)
        {
            SceneManager.LoadScene("BattleGround");
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }
    private IEnumerator RoundStarting()
    {
        ResetPlayers();
        DisablePlayerControls();

        roundNumber++;
        messageText.text = "ROUND " + roundNumber;

        yield return startWait;
    }

    private IEnumerator RoundPlaying()
    {
        messageText.text = "FIGHT!";
        yield return new WaitForSeconds(0.5f);
        EnablePlayerControls();
        messageText.text = string.Empty;

        while (!OnePlayerLeft())
        {
            yield return null;
        }
    }
    private IEnumerator RoundEnding()
    {
        DisablePlayerControls();

        roundWinner = null;

        roundWinner = GetRoundWinner();

        if(roundWinner != null)
        {
            roundWinner.playerWins++;
        }

        gameWinner = GetGameWinner();

        string message = EndMessage();
        messageText.text = message;

        yield return endWait;
    }

    private string EndMessage()
    {
        string message = "DRAW!";

        if(roundWinner != null)
        {
            message = roundWinner.playerColoredText + " WINS THE ROUND";
        }
        message += "\n\n\n\n";
        for (int i = 0; i < players.Length; i++)
        {
            message += players[i].playerColoredText + ": " + players[i].playerWins + " WIN/s \n";
        }

        if(gameWinner != null)
        {
            message = gameWinner.playerColoredText + " WINS THE GAME";
        }

        return message;
    }

    private PlayerManager GetGameWinner()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if(players[i].playerWins == numberOfRounds)
            {
                return players[i];
            }
        }

        return null;
    }

    private PlayerManager GetRoundWinner()
    {
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].playerInstance.activeSelf)
            {
                return players[i];
            }
        }
        return null;
    }

    private bool OnePlayerLeft()
    {
        int numPlayersLeft = 0;

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].playerInstance.activeSelf)
            {
                numPlayersLeft++;
            }
        }

        return numPlayersLeft <= 1;
    }
    private void ResetPlayers()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].Reset();
        }
    }

    private void DisablePlayerControls()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].DisableControls();
        }
    }
    private void EnablePlayerControls()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].EnableControls();
        }
    }
}
