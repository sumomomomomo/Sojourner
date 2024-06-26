using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class OverworldSceneManager : MonoBehaviour
{
    [SerializeField] private string battleSceneName = "BattleScene"; 
    [SerializeField] private IntReference enemyHP;
    [SerializeField] private IntReference enemyMaxHP;
    [SerializeField] private EnemyLoadedTrackerObject enemyLoadedTrackerObject;
    public void LoadBattle()
    {
        Assert.IsTrue(enemyLoadedTrackerObject.LoadedEnemy != null);
        // Initialize ScriptableObjects with provided enemy data
        // enemyHP.Value = enemyObject.MaxHP;
        // enemyMaxHP.Value = enemyObject.MaxHP;
        // Respective enemy battle handlers will do initialization for player bounds etc

        // Fade to battle screen
        StartCoroutine(BattleSceneTransition(battleSceneName));
    }

    private IEnumerator BattleSceneTransition(string sceneName)
    {
        yield return new WaitForSeconds(0.01f);
        SceneManager.LoadScene(sceneName);
    }
}
