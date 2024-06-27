using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyLoader : MonoBehaviour
{
    [SerializeField] private EnemyLoadedTrackerObject currentEnemy;
    [SerializeField] private EnemyHandler enemyHandler;
    void Start()
    {
        if (currentEnemy != null) 
        {
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
}
