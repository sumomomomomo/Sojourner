using System.Collections;
using System.Collections.Generic;
using OpenAI;
using UnityEngine;
using UnityEngine.Assertions;

public class EnemyStateLoader : MonoBehaviour
{
    [SerializeField] private BattleState battleState;
    [SerializeField] private EnemyLoadedTrackerObject currentEnemy;
    [SerializeField] private BattleStrategyTrackerObject battleStrategyTrackerObject;
    [SerializeField] private EnemyHandler enemyHandler;
    [SerializeField] private IntVariable enemyHP;
    [SerializeField] private IntVariable enemyAtk;
    [SerializeField] private IntVariable enemyDef;
    [SerializeField] private FloatVariable enemyAgi;
    [SerializeField] private ChatMessageObjectsTracker chatMessageObjectsTracker;
    void Start()
    {
        if (currentEnemy != null) 
        {
            // Reset Battle State flags
            battleState.ResetAllFlags();

            // Enable all strategies
            battleStrategyTrackerObject.InitAll();

            // Init enemy health
            enemyHP.Value = currentEnemy.LoadedEnemy.MaxHP;
            currentEnemy.LoadedEnemy.SetCurrHP(enemyHP.Value);

            // Init atk def agi values
            enemyAtk.Value = currentEnemy.LoadedEnemy.Atk;
            enemyDef.Value = currentEnemy.LoadedEnemy.Def;
            enemyAgi.Value = currentEnemy.LoadedEnemy.Agility;

            // TODO: player is invisible for 0.5s cos player bound shift animation - add initialization for player bounds?

            // Load Enemy Chat Messages
            chatMessageObjectsTracker.ClearAllMessages();
            foreach (ChatMessageObject chatMessageObject in currentEnemy.LoadedEnemy.DefaultChatMessageObjects)
            {
                chatMessageObjectsTracker.AddChatMessage(chatMessageObject);
            }

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
