using System.Linq;
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
    [SerializeField] private DialogueSpritePair[] dialogueSpritePairs;
    [SerializeField] private AudioClip talkingSound;
    public AudioClip TalkingSound => talkingSound;
    public string[] Dialogue => dialogue;
    public bool HasResponses => responses != null && responses.Length > 0;
    public bool HasSprites => dialogueSpritePairs.Count() == dialogue.Count();
    public bool HasTalkingSound => talkingSound != null;
    public DialogueResponse[] Responses => responses;
    public DialogueSpritePair[] DialogueSpritePairs => dialogueSpritePairs;
}
