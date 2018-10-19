using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerManager
{
    public Color playerColor;
    public Sprite playerSprite;
    public Transform spawnPoint;

    [HideInInspector] public int playerNumber;
    [HideInInspector] public string playerColoredText;
    [HideInInspector] public GameObject playerInstance;
    [HideInInspector] public int playerWins;

    private PlayerController playerController;
   // private GameObject canvasGameObject;


    public void Setup()
    {
        playerController = playerInstance.GetComponent<PlayerController>();
        //canvasGameObject = playerInstance.GetComponentInChildren<Canvas>().gameObject;
        playerController.GetComponent<SpriteRenderer>().sprite = playerSprite;

        playerController.playerNumber = playerNumber;

        playerColoredText = "<color=#" + ColorUtility.ToHtmlStringRGB(playerColor) + ">PLAYER " + playerNumber + "</color>";

        //SpriteRenderer[] renderers = playerInstance.GetComponents<SpriteRenderer>();

        //for (int i = 0; i < renderers.Length; i++)
        //{
        //    renderers[i].color = PlayerColor;
        //}
    }
    public void EnableControls()
    {
        playerController.enabled = true;

        //canvasGameObject.SetActive(true);
    }
    public void DisableControls()
    {
        playerController.enabled = false;

       // canvasGameObject.SetActive(false);
    }
    public void Reset()
    {
        playerInstance.transform.position = spawnPoint.position;
        playerInstance.transform.rotation = spawnPoint.rotation;

        playerInstance.SetActive(false);
        playerInstance.SetActive(true);
    }
}
