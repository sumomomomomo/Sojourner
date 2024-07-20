using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStrategyPlayerMover : MonoBehaviour
{
    [SerializeField] private GameObject playerGroup;
    [SerializeField] private BattleState battleState;
    [SerializeField] private BattleStrategyTrackerObject currentStrategy;

    void Update()
    {
        if (battleState.IsPlayerStrategySelectable())
        {
            playerGroup.transform.position = currentStrategy.GetPlayerTurnPosition();
        }
    }
}
