using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStrategyExecuter : MonoBehaviour
{
    [SerializeField] private BattleStrategyTrackerObject currentStrategy;
    [SerializeField] private BoundTargetInstructionsObject boundTargetInstructionsObject;
    [SerializeField] private PlayerBoundsTarget playerBoundsTarget;
    [SerializeField] private GameEventObject onUpdateBounds;
    public void OnPlayerTurnStart()
    {
        currentStrategy.OnExecuteStrategy?.Invoke();
        boundTargetInstructionsObject.PlayerBoundsTarget = playerBoundsTarget;
        onUpdateBounds.Raise();
    }

    public void OnPlayerTurnEnd()
    {

    }
}
