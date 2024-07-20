using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyLoader : MonoBehaviour
{
    [SerializeField] private BattleState battleState;
    [SerializeField] private EnemyLoadedTrackerObject currentEnemy;
    [SerializeField] private EnemyHandler enemyHandler;
    [SerializeField] private IntReference enemyHP;
    [SerializeField] private IntReference enemyAtk;
    [SerializeField] private IntReference enemyDef;
    void Start()
    {
        if (currentEnemy != null) 
        {
            // Reset Battle State flags
            battleState.ResetAllFlags();

            // Init enemy health
            enemyHP.Value = currentEnemy.LoadedEnemy.MaxHP;
            currentEnemy.LoadedEnemy.SetCurrHP(enemyHP.Value);

            // HARDCODED TODO: account for statuses that affect atk/def
            enemyAtk.Value = currentEnemy.LoadedEnemy.Atk;
            enemyDef.Value = currentEnemy.LoadedEnemy.Def;

            // TODO: player is invisible for 0.5s cos player bound shift animation - add initialization for player bounds

            // Change state for EnemyHandler
            IEnemyHandlerState enemyHandlerState = currentEnemy.LoadedEnemy.EnemyHandlerState;
            Assert.IsNotNull(enemyHandlerState);
            enemyHandler.ChangeState(enemyHandlerState);

            enemyHandler.OnBattleStart();

            Debug.Log("Enemy handler loaded for " + currentEnemy.LoadedEnemy.EnemyName);
        }
        else
        {
            Debug.Log("Enemy not set");    
        }
    }
}
