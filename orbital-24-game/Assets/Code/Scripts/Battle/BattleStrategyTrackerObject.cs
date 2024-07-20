using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Battle/BattleStrategyTracker")]
public class BattleStrategyTrackerObject : ScriptableObject
{
    [SerializeField] private BattleStrategyObject[] strategyOrder;
    [SerializeField] private BattleStrategyObject selectedStrategy;
    [SerializeField] private int strategyIndex = 0;
    public string StrategyName => selectedStrategy != null ? selectedStrategy.StrategyName : "None";
    public UnityEvent OnExecuteStrategy()
    {
        DefaultValidStrategy();
        return selectedStrategy.OnExecuteStrategy;
    }

    public void ToNextStrategy()
    {
        DefaultValidStrategy();
        if (strategyIndex >= strategyOrder.Length - 1) return;
        strategyIndex++;
        BattleStrategyObject nextStrategy = strategyOrder[strategyIndex];
        if (nextStrategy.IsDisabled)
        {
            // try to skip that option
            for (int i = strategyIndex + 1; i < strategyOrder.Length; i++)
            {
                BattleStrategyObject skippedStrategy = strategyOrder[i];
                if (!skippedStrategy.IsDisabled)
                {
                    strategyIndex = i;
                    selectedStrategy = skippedStrategy;
                    return; 
                }
            }
            strategyIndex--;
            return;
        }
        selectedStrategy = strategyOrder[strategyIndex];
    }

    public void ToPreviousStrategy()
    {
        DefaultValidStrategy();
        if (strategyIndex <= 0) return;
        strategyIndex--;
        BattleStrategyObject prevStrategy = strategyOrder[strategyIndex];
        if (prevStrategy.IsDisabled)
        {
            for (int i = strategyIndex - 1; i >= 0; i--)
            {
                BattleStrategyObject skippedStrategy = strategyOrder[i];
                if (!skippedStrategy.IsDisabled)
                {
                    strategyIndex = i;
                    selectedStrategy = skippedStrategy;
                    return; 
                }
            }
            strategyIndex++;
            return;
        }
        selectedStrategy = strategyOrder[strategyIndex];
    }

    private void DefaultValidStrategy()
    {
        if (selectedStrategy == null || selectedStrategy.IsDisabled == true)
        {
            for (int i = 0; i < strategyOrder.Length; i++)
            {
                if (!strategyOrder[i].IsDisabled)
                {
                    selectedStrategy = strategyOrder[i];
                    strategyIndex = i;
                    return;
                }
            }
            Debug.LogError("No valid strategy!");
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
