using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuSceneManager : MonoBehaviour
{
    [SerializeField] private IntReference playerHP;
    [SerializeField] private IntReference playerBaseMaxHP;
    public void NewGame()
    {
        // Init player health
        playerHP.Value = playerBaseMaxHP.Value;

        SceneManager.LoadSceneAsync("OverworldScene");
    }
}
