using UnityEngine;

[CreateAssetMenu(menuName = "Battle/BattleStrategyTracker")]
public class BattleStrategyTrackerObject : ScriptableObject
{
    [SerializeField] private BattleStrategyObject[] strategyOrder;
    [SerializeField] private BattleStrategyObject selectedStrategy;
    [SerializeField] private int strategyIndex = 0;
    public string StrategyName => selectedStrategy != null ? selectedStrategy.StrategyName : "None";

    public void ToNextStrategy()
    {
        if (selectedStrategy == null)
        {
            selectedStrategy = strategyOrder[0];
        }
        else
        {
            strategyIndex++;
            if (strategyIndex == strategyOrder.Length)
            {
                strategyIndex = 0;
            }
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
            strategyIndex--;
            if (strategyIndex == -1)
            {
                strategyIndex = strategyOrder.Length - 1;
            }
            selectedStrategy = strategyOrder[strategyIndex];
        }
    }
}
