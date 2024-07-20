using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStrategyInputHandler : MonoBehaviour
{
    [SerializeField] private BattleStrategyTrackerObject currentStrategy;
    [SerializeField] private BattleState battleState;
    [SerializeField] private GameEventObject onForceChangeTurn;

    void Update()
    {
        if (battleState.IsPlayerStrategySelectable())
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                currentStrategy.ToPreviousStrategy();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                currentStrategy.ToNextStrategy();
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                onForceChangeTurn.Raise();
            }
        }
    }
}
