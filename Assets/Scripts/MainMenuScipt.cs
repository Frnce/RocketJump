using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScipt : MonoBehaviour
{
    public string singlePlayerScene;
    public string pvpScene;

    public void PvpButton()
    {
        SceneManager.LoadScene(pvpScene);
    }
}
