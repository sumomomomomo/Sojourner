using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    private IEnemyHandlerState currentState;
    [SerializeField] private Object _defeatedState;
    private IEnemyHandlerState defeatedState;

    void Start()
    {
        defeatedState = _defeatedState as IEnemyHandlerState;
    }

    public void OnEnemyTurnStart()
    {
        currentState.OnEnemyTurnStart(this);
    }

    public void OnEnemyTurnEnd()
    {
        currentState.OnEnemyTurnEnd(this);
    }

    public void ChangeState(IEnemyHandlerState newState)
    {
        currentState = newState;
    }

    public void ChangeStateToDefeated()
    {
        currentState = defeatedState;
    }
}
