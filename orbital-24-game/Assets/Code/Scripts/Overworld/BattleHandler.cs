using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    [SerializeField] private GameEventObject oninitiateBattle;
    public void InitiateBattle()
    {
        oninitiateBattle.Raise();
    }
}
