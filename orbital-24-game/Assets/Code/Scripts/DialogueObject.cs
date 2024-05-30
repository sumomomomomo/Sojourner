using UnityEngine;

// from Semag Games
[CreateAssetMenu(menuName = "Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea] 
    private string[] dialogue;

    public string[] Dialogue => dialogue;
}
