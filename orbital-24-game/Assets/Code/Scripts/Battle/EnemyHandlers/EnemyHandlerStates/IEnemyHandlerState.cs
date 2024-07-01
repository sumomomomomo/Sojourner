using UnityEngine;

public interface IEnemyHandlerState
{
    abstract void OnEnemyTurnStart(MonoBehaviour monoBehaviour);
    abstract void OnEnemyTurnEnd(MonoBehaviour monoBehaviour);
}