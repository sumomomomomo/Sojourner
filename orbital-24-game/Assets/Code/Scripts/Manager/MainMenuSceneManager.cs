using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuSceneManager : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadSceneAsync("OverworldScene");
    }
}
