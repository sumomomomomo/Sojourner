using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverWatcher : MonoBehaviour
{
    [SerializeField] private IntVariable playerHP;
    [SerializeField] private BoolVariable isGameOver;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameEventObject onGameOver;
    void Start()
    {
        gameOverScreen.SetActive(false);
        StartCoroutine(GameOverEnum());
    }

    private IEnumerator GameOverEnum()
    {
        while (playerHP.Value > 0)
        {
            yield return new WaitForSeconds(0.01f);
        }
        onGameOver.Raise();
        isGameOver.Value = true;
        gameOverScreen.SetActive(true);
    }
}
