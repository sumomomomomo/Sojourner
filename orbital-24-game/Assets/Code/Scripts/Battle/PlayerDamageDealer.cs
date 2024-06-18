using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageDealer : MonoBehaviour
{
    [SerializeField] private IntReference enemyHP;
    [SerializeField] private IntReference enemyDef;
    [SerializeField] private IntReference playerBaseAtk;
    public void dealDamage()
    {
        enemyHP.Value -= Mathf.Max(1, playerBaseAtk.Value - enemyDef.Value);
    }
}
