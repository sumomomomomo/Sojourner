using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleWinWatcher : MonoBehaviour
{
    [SerializeField] private IntVariable enemyHP;
    [SerializeField] private BattleState battleState;
    [SerializeField] private GameEventObject onBattleWin;
    void Start()
    {
        battleState.SetBattleWin(false);
        StartCoroutine(WinEnum());
    }

    private IEnumerator WinEnum()
    {
        yield return new WaitForSeconds(1f);
        while (enemyHP.Value > 0)
        {
            yield return new WaitForSeconds(0.01f);
        }
        battleState.SetBattleWin(true);
        onBattleWin.Raise();
    }
}
