using System;
using UnityEngine;

public interface IScriptedAttack
{
    abstract void OnBattleStart(GameObject player);
    abstract void OnEnemyAttackStart();
    abstract void OnEnemyAttackEnd();
    abstract void SetVariant(int variant);
}
