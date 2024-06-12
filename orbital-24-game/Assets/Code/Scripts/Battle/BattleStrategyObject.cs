using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Battle/BattleStrategy")]
public class BattleStrategyObject : ScriptableObject
{
    [SerializeField] private string strategyName;
    [SerializeField] private UnityEvent onExecuteStrategy;
    public string StrategyName => strategyName;
    public UnityEvent OnExecuteStrategy => onExecuteStrategy;
}
