using UnityEngine;

// Adapted from Semag Games
/// <summary>
/// Represents a singular dialogue data, and associated responses.
/// </summary>
[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private DialogueResponse[] responses;

    public string[] Dialogue => dialogue;
    public bool HasResponses => responses != null && responses.Length > 0;
    public DialogueResponse[] Responses => responses;
}
