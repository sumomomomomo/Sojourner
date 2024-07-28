using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    [SerializeField] private IntVariable enemyHP;
    [SerializeField] private BattleState battleState;
    [SerializeField] private TurnHandler turnHandler;
    [SerializeField] private GameObject player;
    private IEnemyHandlerState currentState;
    public void OnEnemyTurnStart()
    {
        currentState.OnEnemyTurnStart(this);
    }

    public void OnEnemyTurnEnd()
    {
        currentState.OnEnemyTurnEnd(this);
    }

    public void OnBattleStart()
    {
        currentState.OnBattleStart(this, turnHandler, player);
    }

    public void OnPlayerWin()
    {
        currentState.OnPlayerWin(this);
    }

    public void OnPlayerRun()
    {
        currentState.OnPlayerRun(this, battleState);
    }

    public void OnTakeDamage()
    {
        currentState.OnTakeDamage(this, enemyHP, battleState);
    }

    public bool CheckLLMResponse(string content)
    {
        return currentState.CheckLLMResponse(this, content);
    }

    public void HandleLLMResponse(string content)
    {
        currentState.HandleLLMResponse(this, content);
    }

    public void ChangeState(IEnemyHandlerState newState)
    {
        Debug.Log("State changed from " + currentState + " to " + newState); 
        currentState = newState;
    }
}
