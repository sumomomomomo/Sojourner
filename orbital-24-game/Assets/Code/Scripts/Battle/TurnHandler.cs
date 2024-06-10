using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text countdownTimer;
    [SerializeField] private FloatReference enemyAgility;
    [SerializeField] private FloatReference playerAgility;
    [SerializeField] private FloatReference timeLeftToNextTurn;
    [SerializeField] private BoolReference isPlayerTurn;

    private float currTurnLength(float playerAgility, float enemyAgility, bool isPlayerTurn)
    {
        if (isPlayerTurn)
        {
            return 5;
        }
        return Math.Max(10, 10 + enemyAgility - playerAgility);
    }
    void Start()
    {
        // TODO always hardcoded for enemy to move first?
        isPlayerTurn.Value = false;
        timeLeftToNextTurn.Value = currTurnLength(playerAgility.Value, enemyAgility.Value, false);
    }

    void Update()
    {
        timeLeftToNextTurn.Value -= Time.deltaTime;
        if (timeLeftToNextTurn.Value <= 0)
        {
            ChangeTurn();
        }
        else
        {
            countdownTimer.text = ((int) Math.Floor(timeLeftToNextTurn.Value)).ToString() + "s (" + (isPlayerTurn.Value ? "Player" : "Enemy") + ")";
        }
    }

    private void ChangeTurn()
    {
        isPlayerTurn.Value = !isPlayerTurn.Value;
        timeLeftToNextTurn.Value = currTurnLength(playerAgility.Value, enemyAgility.Value, isPlayerTurn.Value);
    }
}
