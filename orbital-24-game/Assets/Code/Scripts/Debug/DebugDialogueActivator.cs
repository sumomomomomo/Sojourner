using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDialogueActivator : MonoBehaviour
{
    public DialogueObject dialogueObject;
    public Player player;

    [SerializeField] [TextArea] private string developerComments;

    [InspectorButton("OnButtonClicked")]
    public bool InitiateTalk;

    private void OnButtonClicked()
    {
        if (dialogueObject != null)
        {
            player.DialogueUI.ShowDialogue(dialogueObject);
        }
    }
}