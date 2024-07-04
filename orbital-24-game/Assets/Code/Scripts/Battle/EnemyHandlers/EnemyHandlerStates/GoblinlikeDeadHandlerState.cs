using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Handler States/Goblinlike Dead")]
public class GoblinlikeDeadHandlerState : ScriptableObject, IEnemyHandlerState
{
    [SerializeField] private GameObject winText;
    public void OnEnemyTurnStart(MonoBehaviour monoBehaviour)
    {
        Debug.Log("EnemyTurnStart Dead");
        Instantiate(winText);
    }
    public void OnEnemyTurnEnd(MonoBehaviour monoBehaviour)
    {
        Debug.Log("EnemyTurnEnd Dead");
    }
}
