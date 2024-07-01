using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStrategyPlayerMover : MonoBehaviour
{
    [SerializeField] private GameObject playerGroup;
    [SerializeField] private BoolVariable isPlayerTurn;
    [SerializeField] private BoolVariable isFreezeTurn;
    [SerializeField] private BattleStrategyTrackerObject currentStrategy;

    void Update()
    {
        if (isPlayerTurn.Value && !isFreezeTurn.Value)
        {
            playerGroup.transform.position = currentStrategy.GetPlayerTurnPosition();
        }
    }
}
