using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuSceneManager : MonoBehaviour
{
    [SerializeField] private IntReference playerHP;
    [SerializeField] private IntReference playerBaseMaxHP;
    [SerializeField] private FloatVariable playerCoordinatesX;
    [SerializeField] private FloatVariable playerCoordinatesY;
    [SerializeField] private FloatVariable startingPlayerCoordinatesX;
    [SerializeField] private FloatVariable startingPlayerCoordinatesY;
    public void NewGame()
    {
        // Init player health
        playerHP.Value = playerBaseMaxHP.Value;

        // Init player coordinates
        playerCoordinatesX.Value = startingPlayerCoordinatesX.Value;
        playerCoordinatesY.Value = startingPlayerCoordinatesY.Value;

        SceneManager.LoadSceneAsync("OverworldScene");
    }
}
