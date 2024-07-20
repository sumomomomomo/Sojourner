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
    [SerializeField] private FloatReference timeLeftToNextTurn;
    [SerializeField] private FloatReference timeLeftToNextTurnMax;
    [SerializeField] private BattleState battleState;

    [SerializeField] private GameEventObject onEnemyTurnStart;
    [SerializeField] private GameEventObject onEnemyTurnEnd;
    [SerializeField] private GameEventObject onPlayerTurnStart;
    [SerializeField] private GameEventObject onPlayerTurnEnd;

    [SerializeField] private BattleWinWatcher battleWinWatcher;

    private float currTurnLength(float playerAgility, float enemyAgility, bool isPlayerTurn)
    {
        if (isPlayerTurn)
        {
            return 5;
        }
        return Math.Max(minEnemyTurnTime.Value, 10 + enemyAgility - playerAgility);
    }
    void Start()
    {
        // Hardcoded
        battleState.OnEndChangeTurn();
        battleState.SetToPlayerTurn();
        ChangeTimeLeftValues(currTurnLength(playerAgility.Value, enemyLoadedTrackerObject.LoadedEnemy.Agility, false));
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
        battleState.OnStartChangeTurn();
        timeLeftToNextTurn.Value = 0;
        StartCoroutine(ChangeTurnEnum());
    }

    private IEnumerator ChangeTurnEnum()
    {
        if (battleState.IsPlayerTurn()) // player -> enemy
        {
            onPlayerTurnEnd.Raise();
            yield return StartEnemyTurnWhenPossible();
        }
        else // enemy -> player
        {
            onEnemyTurnEnd.Raise();
            yield return StartPlayerTurnWhenPossible();
        }
        battleState.FlipIsPlayerTurn();
        ChangeTimeLeftValues(currTurnLength(playerAgility.Value, enemyLoadedTrackerObject.LoadedEnemy.Agility, battleState.IsPlayerTurn()));
        battleState.OnEndChangeTurn();
    }

    private IEnumerator StartPlayerTurnWhenPossible()
    {
        while (!battleState.CanStartPlayerTurn())
        {
            yield return null;
        }
        onPlayerTurnStart.Raise();
    }

    private IEnumerator StartEnemyTurnWhenPossible()
    {
        while (!battleState.CanStartEnemyTurn())
        {
            yield return null;
        }
        if (!battleWinWatcher.ForceCheckBattleWin())
        {
            onEnemyTurnStart.Raise();
        }
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

        // Set enemy flag to be dead
        enemyLoadedTrackerObject.LoadedEnemy.OnBattleWin();

    }

}
