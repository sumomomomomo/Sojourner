using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageHandler : MonoBehaviour
{
    [SerializeField] private IntReference playerHP;
    [SerializeField] private IntReference playerBaseAtk;
    [SerializeField] private IntReference playerBaseDef;
    [SerializeField] private BoolReference isPlayerDefending;

    [SerializeField] private IntReference enemyHP;
    [SerializeField] private IntReference enemyDef;
    [SerializeField] private IntReference enemyAtk;
    public void dealDamage()
    {
        enemyHP.Value -= Mathf.Max(1, playerBaseAtk.Value - enemyDef.Value);
        isPlayerDefending.Value = false;
    }

    public void takeDamage()
    {
        // TODO: bullet modifier
        int effDef = isPlayerDefending.Value ? playerBaseDef.Value * 2 : playerBaseDef.Value;
        playerHP.Value -= Mathf.Max(1, enemyAtk.Value - effDef);
        isPlayerDefending.Value = false;
    }
}
