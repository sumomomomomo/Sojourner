using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OverworldSceneManager : MonoBehaviour
{
    [SerializeField] private string battleSceneName = "BattleScene"; 
    [SerializeField] private IntReference enemyHP;
    [SerializeField] private IntReference enemyMaxHP;

    [SerializeField] private FloatReference boundWidth;
    [SerializeField] private FloatReference boundHeight;
    [SerializeField] private FloatReference boundOriginXTranslation;
    [SerializeField] private FloatReference boundOriginYTranslation;
    public void LoadBattle(EnemyObject enemyObject)
    {
        // Initialize ScriptableObjects with provided enemy data
        enemyHP.Value = enemyObject.MaxHP;
        enemyMaxHP.Value = enemyObject.MaxHP;
        // TODO enemy sprite, agility, attack patterns

        // TODO hardcoded: bounding box dimensions are hardcoded
        boundWidth.Value = 200;
        boundHeight.Value = 200;
        boundOriginXTranslation.Value = 0;
        boundOriginYTranslation.Value = 0;

        // Fade to battle screen
        StartCoroutine(BattleSceneTransition(battleSceneName));
    }

    private IEnumerator BattleSceneTransition(string sceneName)
    {
        yield return new WaitForSeconds(0.01f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
