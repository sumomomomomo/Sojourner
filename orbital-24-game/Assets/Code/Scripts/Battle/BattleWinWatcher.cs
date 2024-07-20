using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleWinWatcher : MonoBehaviour
{
    [SerializeField] private IntVariable enemyHP;
    [SerializeField] private BattleState battleState;
    [SerializeField] private GameEventObject onBattleWin;
    private bool hasRaised;
    void Start()
    {
        hasRaised = false;
        battleState.SetBattleWin(false);
        StartCoroutine(WinEnum());
    }

    private IEnumerator WinEnum()
    {
        yield return new WaitForSeconds(1f);
        while (!ForceCheckBattleWin())
        {
            yield return new WaitForSeconds(0.01f);
        }
    }

    public bool ForceCheckBattleWin()
    {
        if (hasRaised)
        {
            return true;
        }
        if (enemyHP.Value > 0)
        {
            return false;
        }
        battleState.SetBattleWin(true);
        onBattleWin.Raise();
        return true;
    }
}