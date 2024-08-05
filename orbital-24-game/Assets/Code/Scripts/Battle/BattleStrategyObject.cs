using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Battle/BattleStrategy")]
public class BattleStrategyObject : ScriptableObject
{
    [SerializeField] private string strategyName;
    [SerializeField] private string originalDisplayText;
    private string displayText;
    public string DisplayText => displayText;
    [SerializeField] private Color originalColor;
    private Color color;
    public Color Color => color;
    [SerializeField] private bool isDisabled = false;
    public bool IsDisabled => isDisabled;
    [SerializeField] private UnityEvent onExecuteStrategy;
    public string StrategyName => strategyName;
    public UnityEvent OnExecuteStrategy => onExecuteStrategy;
    [SerializeField] private float playerTurnXCoordinate;
    public float PlayerTurnXCoordinate => playerTurnXCoordinate;
    [SerializeField] private float playerTurnYCoordinate;
    public float PlayerTurnYCoordinate => playerTurnYCoordinate;

    public void Init()
    {
        displayText = originalDisplayText;
        color = originalColor;
        Enable();
    }
    public void Disable()
    {
        isDisabled = true;
    }

    public void Enable()
    {
        isDisabled = false;
    }

    public void SetText(string newText)
    {
        displayText = newText;
    }

    public void SetColor(Color color)
    {
        this.color = color;
    }
}
