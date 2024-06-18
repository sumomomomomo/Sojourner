using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDamageTaker : MonoBehaviour
{
    [SerializeField] private IntReference playerHP;
    [SerializeField] private IntReference playerBaseDef;
    [SerializeField] private BoolReference isPlayerDefending;
    [SerializeField] private BoolReference isPlayerInvulnerable;
    [SerializeField] private UnityEvent onPlayerTakeDamage;
    [SerializeField] private IntReference enemyAtk;
    public void takeDamage(float damageModifier)
    {
        if (isPlayerInvulnerable.Value) return;
        int effDef = isPlayerDefending.Value ? playerBaseDef.Value * 2 : playerBaseDef.Value;
        playerHP.Value = playerHP.Value - (int) Mathf.Floor(Mathf.Max(1, enemyAtk.Value * damageModifier - effDef));
        onPlayerTakeDamage?.Invoke();
    }
    public void takeDamage()
    {
        takeDamage(1);
    }
}
