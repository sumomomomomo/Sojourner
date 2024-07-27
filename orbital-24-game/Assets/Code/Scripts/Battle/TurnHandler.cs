using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TurnHandler : MonoBehaviour
{
    [SerializeField] private EnemyLoadedTrackerObject enemyLoadedTrackerObject;

    [SerializeField] private FloatReference minEnemyTurnTime;
    [SerializeField] private FloatReference playerAgility;
    [SerializeField] private FloatReference enemyAgility;
    [SerializeField] private FloatReference timeLeftToNextTurn;
    [SerializeField] private FloatReference timeLeftToNextTurnMax;
    [SerializeField] private BattleState battleState;

    [SerializeField] private GameEventObject onEnemyTurnStart;
    [SerializeField] private GameEventObject onEnemyTurnEnd;
    [SerializeField] private GameEventObject onPlayerTurnStart;
    [SerializeField] private GameEventObject onPlayerTurnEnd;

    [SerializeField] private BattleWinWatcher battleWinWatcher;

    private Coroutine changeTurnCoroutine;

    private float CurrTurnLength(float playerAgility, float enemyAgility, bool isPlayerTurn)
    {
        if (isPlayerTurn)
        {
            return Math.Max(5, 5 + playerAgility - enemyAgility);
        }
        else
        {
            return Math.Max(3, Math.Max(minEnemyTurnTime.Value, 10 + enemyAgility - playerAgility));
        }
    }
    void Start()
    {
        // Hardcoded
        battleState.SetChangeTurnExecutingToFalse();
        battleState.SetToPlayerTurn();
        ChangeTimeLeftValues(10);
        onPlayerTurnStart.Raise();
    }

    void Update()
    {
        if (battleState.IsTurnHandlerActive())
        {
            timeLeftToNextTurn.Value -= Time.deltaTime;
            if (timeLeftToNextTurn.Value <= 0)
            {
                ChangeTurn();
            }
        }
    }

    private void ChangeTimeLeftValues(float time)
    {
        timeLeftToNextTurnMax.Value = time;
        timeLeftToNextTurn.Value = time;
    }

    public void ChangeTurn()
    {
        Debug.Log("Change turn called");
        battleState.SetChangeTurnExecutingToTrue();
        timeLeftToNextTurn.Value = 0;
        changeTurnCoroutine = StartCoroutine(ChangeTurnEnum());
    }

    private IEnumerator ChangeTurnEnum()
    {
        Debug.Log("Change turn enum start");
        if (battleState.IsPlayerTurn()) // player -> enemy
        {
            onPlayerTurnEnd.Raise();
            while (!battleState.CanStartEnemyTurn())
            {
                yield return null;
            }
            onEnemyTurnStart.Raise();
        }
        else // enemy -> player
        {
            onEnemyTurnEnd.Raise();
            while (!battleState.CanStartPlayerTurn())
            {
                yield return null;
            }
            onPlayerTurnStart.Raise();
        }
        battleState.FlipIsPlayerTurn();
        ChangeTimeLeftValues(CurrTurnLength(playerAgility.Value, enemyAgility.Value, battleState.IsPlayerTurn()));
        battleState.SetChangeTurnExecutingToFalse();
        Debug.Log("Change turn enum end");
    }

    public void OnBattleLose()
    {
        Debug.Log("OnBattleLose");
        //onPlayerTurnEnd.Raise(); //buggy
        onEnemyTurnEnd.Raise();
    }

    public void OnBattleWin()
    {
        Debug.Log("OnBattleWin");
        //onPlayerTurnEnd.Raise(); //buggy
        //onEnemyTurnEnd.Raise();
    }

    public void OnPlayerTalk()
    {
        // Force kill TurnChangeEnum
        // Enum will be stuck as enemy cannot start turn
        StartCoroutine(KillChangeTurnEnum());
        // And set player turn time to 20s
        ChangeTimeLeftValues(20f);
    }

    private IEnumerator KillChangeTurnEnum()
    {
        while (true)
        {
            if (changeTurnCoroutine != null)
            {
                Debug.Log("killed change turn enum");
                StopCoroutine(changeTurnCoroutine);
                battleState.SetChangeTurnExecutingToFalse();
                break;
            }
            else
            {
                yield return null;
            }
        }
    }

    public void SetTimeLeftValues(float timeLeft)
    {
        ChangeTimeLeftValues(timeLeft);
    }

}
