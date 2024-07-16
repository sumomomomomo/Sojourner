using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

/// <summary>
/// This class is to be bound to the main canvas containing dialogue UI elements.
/// Handles user input.
/// </summary>
public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private BoolVariable isDialogueBoxOpen;

    private DialogueUIResponseHandler responseHandler;
    private DialogueUITypewriterEffect typewriterEffect;
    void Start()
    {
        typewriterEffect = GetComponent<DialogueUITypewriterEffect>();
        responseHandler = GetComponent<DialogueUIResponseHandler>();
        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        isDialogueBoxOpen.Value = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            yield return typewriterEffect.Run(dialogue, textLabel);

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) 
            {
                break;
            }

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else
        {
            CloseDialogueBox();
        }
    }

    private void CloseDialogueBox()
    {
        isDialogueBoxOpen.Value = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
