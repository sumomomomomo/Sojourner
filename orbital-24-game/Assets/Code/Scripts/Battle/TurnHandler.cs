using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class TurnHandler : MonoBehaviour
{
    [SerializeField] private FloatReference minEnemyTurnTime;
    [SerializeField] private FloatReference enemyAgility;
    [SerializeField] private FloatReference playerAgility;
    [SerializeField] private FloatReference timeLeftToNextTurn;
    [SerializeField] private BoolReference isPlayerTurn;
    [SerializeField] private BattleStrategyTrackerObject currentStrategy;

    [SerializeField] private GameEventObject onEnemyTurnStart;
    [SerializeField] private GameEventObject onEnemyTurnEnd;

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
        // TODO always hardcoded for enemy to move first?
        //isPlayerTurn.Value = false;
        //timeLeftToNextTurn.Value = currTurnLength(playerAgility.Value, enemyAgility.Value, false);
        isPlayerTurn.Value = true;
        timeLeftToNextTurn.Value = 2;
    }

    void Update()
    {
        timeLeftToNextTurn.Value -= Time.deltaTime;
        if (timeLeftToNextTurn.Value <= 0)
        {
            ChangeTurn();
        }
    }

    private void ChangeTurn()
    {
        if (isPlayerTurn.Value) // player -> enemy
        {
            onEnemyTurnStart.Raise();
        }
        else // enemy -> player
        {
            onEnemyTurnEnd.Raise();
            currentStrategy.OnExecuteStrategy?.Invoke();
        }
        isPlayerTurn.Value = !isPlayerTurn.Value;
        timeLeftToNextTurn.Value = currTurnLength(playerAgility.Value, enemyAgility.Value, isPlayerTurn.Value);
    }

    // Hardcoded?


}
