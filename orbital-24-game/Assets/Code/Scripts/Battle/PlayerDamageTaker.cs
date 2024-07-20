using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDamageTaker : MonoBehaviour
{
    [SerializeField] private IntReference playerHP;
    [SerializeField] private IntReference playerBaseDef;
    [SerializeField] private BattleState battleState;
    [SerializeField] private GameEventObject onPlayerTakeDamage;
    [SerializeField] private IntReference enemyAtk;
    public void takeDamage(float damageModifier)
    {
        if (battleState.IsPlayerInvulnerable()) return;
        int effDef = battleState.IsPlayerDefending() ? playerBaseDef.Value * 2 : playerBaseDef.Value;
        playerHP.Value = playerHP.Value - (int) Mathf.Floor(Mathf.Max(1, enemyAtk.Value * damageModifier - effDef));
        onPlayerTakeDamage.Raise();
    }
    public void takeDamage()
    {
        takeDamage(1);
    }
}
