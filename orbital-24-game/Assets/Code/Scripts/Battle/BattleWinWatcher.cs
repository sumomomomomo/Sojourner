using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleWinWatcher : MonoBehaviour
{
    [SerializeField] private IntVariable enemyHP;
    [SerializeField] private BoolVariable isBattleWin;
    [SerializeField] private GameObject gameWinText;
    [SerializeField] private GameEventObject onBattleWin;
    void Start()
    {
        gameWinText.SetActive(false);
        isBattleWin.Value = false;
        StartCoroutine(WinEnum());
    }

    private IEnumerator WinEnum()
    {
        yield return new WaitForSeconds(1f);
        while (enemyHP.Value > 0)
        {
            yield return new WaitForSeconds(0.01f);
        }
        onBattleWin.Raise();
        isBattleWin.Value = true;
        gameWinText.SetActive(true);
    }
}
