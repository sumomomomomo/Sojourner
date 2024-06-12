using UnityEngine;

[CreateAssetMenu(menuName = "Battle/BattleStrategy")]
public class BattleStrategyObject : ScriptableObject
{
    [SerializeField] private string strategyName;
    public string StrategyName => strategyName;
}
