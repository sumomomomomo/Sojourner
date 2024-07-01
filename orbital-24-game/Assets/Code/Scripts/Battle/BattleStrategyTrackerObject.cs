using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Battle/BattleStrategyTracker")]
public class BattleStrategyTrackerObject : ScriptableObject
{
    [SerializeField] private BattleStrategyObject[] strategyOrder;
    [SerializeField] private BattleStrategyObject selectedStrategy;
    [SerializeField] private int strategyIndex = 0;
    public string StrategyName => selectedStrategy != null ? selectedStrategy.StrategyName : "None";

    public UnityEvent OnExecuteStrategy => selectedStrategy.OnExecuteStrategy;

    public void ToNextStrategy()
    {
        if (selectedStrategy == null)
        {
            selectedStrategy = strategyOrder[0];
        }
        else
        {
            if (strategyIndex >= strategyOrder.Length - 1) return;
            strategyIndex++;
            selectedStrategy = strategyOrder[strategyIndex];
        }
    }

    public void ToPreviousStrategy()
    {
        if (selectedStrategy == null)
        {
            selectedStrategy = strategyOrder[0];
        }
        else
        {
            if (strategyIndex <= 0) return;
            strategyIndex--;
            selectedStrategy = strategyOrder[strategyIndex];
        }
    }

    public Vector2 GetPlayerTurnPosition()
    {
        if (selectedStrategy == null)
        {
            selectedStrategy = strategyOrder[0];
        }
        return new Vector2(selectedStrategy.PlayerTurnXCoordinate, selectedStrategy.PlayerTurnYCoordinate);
    }
}
