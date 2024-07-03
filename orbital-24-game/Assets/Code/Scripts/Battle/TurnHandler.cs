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
    [SerializeField] private BoolReference isFreezeTurn;
    [SerializeField] private BoolReference isBattleLose;
    [SerializeField] private BoolReference isBattleWin;

    [SerializeField] private GameEventObject onEnemyTurnStart;
    [SerializeField] private GameEventObject onEnemyTurnEnd;
    [SerializeField] private GameEventObject onPlayerTurnStart;
    [SerializeField] private GameEventObject onPlayerTurnEnd;

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
        isPlayerTurn.Value = true;
        timeLeftToNextTurn.Value = currTurnLength(playerAgility.Value, enemyAgility.Value, false);
        onPlayerTurnStart.Raise();

    }

    void Update()
    {
        if (!isFreezeTurn.Value && !isBattleLose.Value && !isBattleWin.Value)
        {
            timeLeftToNextTurn.Value -= Time.deltaTime;
            if (timeLeftToNextTurn.Value <= 0)
            {
                ChangeTurn();
            }
        }
    }

    public void ChangeTurn()
    {
        StartCoroutine(ChangeTurnEnum());
    }

    private IEnumerator ChangeTurnEnum()
    {
        if (isPlayerTurn.Value) // player -> enemy
        {
            onPlayerTurnEnd.Raise();
            yield return new WaitForSeconds(0.5f); // TODO redo this later
            onEnemyTurnStart.Raise();
        }
        else // enemy -> player
        {
            onEnemyTurnEnd.Raise();
            onPlayerTurnStart.Raise();
        }
        isPlayerTurn.Value = !isPlayerTurn.Value;
        timeLeftToNextTurn.Value = currTurnLength(playerAgility.Value, enemyAgility.Value, isPlayerTurn.Value);
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

}
