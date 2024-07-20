using UnityEngine;

public interface IEnemyHandlerState
{
    abstract void OnEnemyTurnStart(MonoBehaviour monoBehaviour);
    abstract void OnEnemyTurnEnd(MonoBehaviour monoBehaviour);
    abstract void OnBattleStart(MonoBehaviour monoBehaviour);
    abstract void OnPlayerWin(MonoBehaviour monoBehaviour);
    abstract void OnTakeDamage(MonoBehaviour monoBehaviour, IntVariable enemyHP, BattleState battleState);
}