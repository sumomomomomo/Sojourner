using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RetryHandler : MonoBehaviour
{
    [SerializeField] private IntReference playerHP;
    [SerializeField] private GameEventObject onRetryBattle;
    private int startingPlayerHP;
    void Start()
    {
        Debug.Log("RetryHandler start");
        startingPlayerHP = playerHP.Value;
    }

    public void RetryBattle()
    {
        playerHP.Value = Mathf.Max(1, startingPlayerHP);
        onRetryBattle.Raise();
    }
}
