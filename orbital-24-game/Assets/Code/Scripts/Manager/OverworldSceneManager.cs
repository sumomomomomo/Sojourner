using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldSceneManager : MonoBehaviour
{
    [SerializeField] private string battleSceneName = "BattleScene"; 

    [SerializeField] private IntReference enemyHP;
    public void LoadBattle(EnemyObject enemyObject)
    {
        // Initialize ScriptableObjects with provided enemy data
        enemyHP.Value = enemyObject.MaxHP;
        // TODO enemy sprite, agility, attack patterns
        // Fade to battle screen
        StartCoroutine(BattleSceneTransition(battleSceneName));
    }

    private IEnumerator BattleSceneTransition(string sceneName)
    {
        yield return new WaitForSeconds(0.01f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
