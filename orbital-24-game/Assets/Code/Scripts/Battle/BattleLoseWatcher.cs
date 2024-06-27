using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleLoseWatcher : MonoBehaviour
{
    [SerializeField] private IntVariable playerHP;
    [SerializeField] private BoolVariable isBattleLose;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameEventObject onBattleLose;
    void Start()
    {
        gameOverScreen.SetActive(false);
        isBattleLose.Value = false;
        StartCoroutine(GameOverEnum());
    }

    private IEnumerator GameOverEnum()
    {
        while (playerHP.Value > 0)
        {
            yield return new WaitForSeconds(0.01f);
        }
        onBattleLose.Raise();
        isBattleLose.Value = true;
        gameOverScreen.SetActive(true);
    }
}
