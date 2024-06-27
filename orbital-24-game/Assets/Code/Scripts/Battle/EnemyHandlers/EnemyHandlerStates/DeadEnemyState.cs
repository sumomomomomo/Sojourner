using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Handler States/Dead")]
public class DeadHandlerState : ScriptableObject, IEnemyHandlerState
{
    public void OnEnemyTurnEnd(MonoBehaviour monoBehaviour)
    {
        Debug.Log("EnemyTurnEnd Dead");
    }

    public void OnEnemyTurnStart(MonoBehaviour monoBehaviour)
    {
        Debug.Log("EnemyTurnEnd Start");
    }
}
