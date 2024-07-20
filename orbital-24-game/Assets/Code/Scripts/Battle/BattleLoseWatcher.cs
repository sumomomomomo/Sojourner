using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLoseWatcher : MonoBehaviour
{
    [SerializeField] private IntVariable playerHP;
    [SerializeField] private BattleState battleState;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameEventObject onBattleLose;
    void Start()
    {
        gameOverScreen.SetActive(false);
        battleState.SetBattleLose(false);
        StartCoroutine(GameOverEnum());
    }

    private IEnumerator GameOverEnum()
    {
        yield return new WaitForSeconds(1f);
        while (playerHP.Value > 0)
        {
            yield return new WaitForSeconds(0.01f);
        }
        onBattleLose.Raise();
        battleState.SetBattleLose(true);
        gameOverScreen.SetActive(true);
    }
}
