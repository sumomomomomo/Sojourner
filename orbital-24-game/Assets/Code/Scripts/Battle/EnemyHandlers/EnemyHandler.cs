using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
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
        currentState.OnBattleStart(this);
    }

    public void OnPlayerWin()
    {
        currentState.OnPlayerWin(this);
    }

    public void ChangeState(IEnemyHandlerState newState)
    {
        Debug.Log("State changed from " + currentState + " to " + newState); 
        currentState = newState;
    }
}
