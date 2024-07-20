using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Battle/BattleStrategy")]
public class BattleStrategyObject : ScriptableObject
{
    [SerializeField] private string strategyName;
    [SerializeField] private bool isDisabled = false;
    public bool IsDisabled => isDisabled;
    [SerializeField] private UnityEvent onExecuteStrategy;
    public string StrategyName => strategyName;
    public UnityEvent OnExecuteStrategy => onExecuteStrategy;
    [SerializeField] private float playerTurnXCoordinate;
    public float PlayerTurnXCoordinate => playerTurnXCoordinate;
    [SerializeField] private float playerTurnYCoordinate;
    public float PlayerTurnYCoordinate => playerTurnYCoordinate;
}
