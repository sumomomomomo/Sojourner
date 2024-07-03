using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleSceneSceneManager : MonoBehaviour
{
    [SerializeField] private string mainMenuSceneName = "MainMenuScene"; 
    [SerializeField] private string battleSceneName = "BattleScene"; 
    [SerializeField] private string overworldSceneName = "OverworldScene";
    public void RetryBattle()
    {
        SceneManager.LoadScene(battleSceneName);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void ReturnToOverworld()
    {
        SceneManager.LoadScene(overworldSceneName);
    }
}