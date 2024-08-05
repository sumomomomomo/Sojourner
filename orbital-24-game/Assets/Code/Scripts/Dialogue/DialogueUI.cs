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
    [SerializeField] private DialogueUIResponseHandler responseHandler;
    [SerializeField] private DialogueUITypewriterEffect typewriterEffect;
    [SerializeField] private DialogueUITypewriterEffectSound typewriterEffectSound;
    [SerializeField] private DialogueUISpriteHandler spriteHandler;

    void Start()
    {
        CloseDialogueBox();
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        if (dialogueObject == null)
        {
            CloseDialogueBox();
            return;
        }
        isDialogueBoxOpen.Value = true;
        dialogueBox.SetActive(true);

        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
        {
            if (dialogueObject.HasSprites)
            {
                spriteHandler.ShowSprites(dialogueObject.DialogueSpritePairs[i]);
            }

            string dialogue = dialogueObject.Dialogue[i];
            if (dialogueObject.HasTalkingSound)
            {
                typewriterEffectSound.SetAudioClip(dialogueObject.TalkingSound[i]);
            }

            yield return RunTypingEffect(dialogue, dialogueObject);

            textLabel.text = dialogue;

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) 
            {
                break;
            }

            yield return null; // to guard against instant skipping to bottom line, when spacebar is pressed

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        if (dialogueObject.HasResponses)
        {
            if (dialogueObject.Responses[0].ResponseText == "")
            {
                dialogueObject.Responses[0].OnPickedResponse.Invoke();
                CloseDialogueBox();
            }
            else
            {
                responseHandler.ShowResponses(dialogueObject.Responses);
            }
        }
        else
        {
            CloseDialogueBox();
        }
    }

    private IEnumerator RunTypingEffect(string dialogue, DialogueObject dialogueObject)
    {
        typewriterEffect.Run(dialogue, textLabel);
        if (typewriterEffectSound != null && dialogueObject.HasTalkingSound)
        {
            typewriterEffectSound.Run();
        }
        
        while (typewriterEffect.IsRunning)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                typewriterEffect.Stop();
            }
        }
    }

    private void CloseDialogueBox()
    {
        spriteHandler.HideSprites();
        isDialogueBoxOpen.Value = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
