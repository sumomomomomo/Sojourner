using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyLoader : MonoBehaviour
{
    [SerializeField] private EnemyLoadedTrackerObject currentEnemy;
    [SerializeField] private EnemyHandler enemyHandler;
    [SerializeField] private IntReference enemyHP;
    [SerializeField] private IntReference enemyAtk;
    [SerializeField] private IntReference enemyDef;
    private GameObject loadedEnemySprite;
    void Start()
    {
        if (currentEnemy != null) 
        {
            // Init enemy health
            enemyHP.Value = currentEnemy.LoadedEnemy.MaxHP;

            // HARDCODED TODO: account for statuses that affect atk/def
            enemyAtk.Value = currentEnemy.LoadedEnemy.Atk;
            enemyDef.Value = currentEnemy.LoadedEnemy.Def;

            // TODO: player is invisible for 0.5s cos player bound shift animation - add initialization for player bounds

            // Load enemy sprite/graphics etc
            loadedEnemySprite = Instantiate(currentEnemy.LoadedEnemy.SpritePrefab);

            // Change state for EnemyHandler
            IEnemyHandlerState enemyHandlerState = currentEnemy.LoadedEnemy.EnemyHandlerState;
            Assert.IsNotNull(enemyHandlerState);
            enemyHandler.ChangeState(enemyHandlerState);
            Debug.Log("Enemy handler loaded for " + currentEnemy.LoadedEnemy.EnemyName);
        }
        else
        {
            Debug.Log("Enemy not set");    
        }
    }

    public void UpdateEnemySprite(GameObject newSpritePrefab)
    {
        Assert.IsNotNull(loadedEnemySprite);
        Destroy(loadedEnemySprite);
        Instantiate(newSpritePrefab);
    }
}
