using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Data structure representing information on how to handle decisions made during dialogue sequences.
/// Compose within DialogueObject.
/// </summary>
[System.Serializable]
public class DialogueResponse
{
    [SerializeField] private string responseText;
    [SerializeField] private DialogueObject nextDialogue;
    [SerializeField] private UnityEvent onPickedResponse;

    public UnityEvent OnPickedResponse => onPickedResponse;
    public string ResponseText => responseText;
    public DialogueObject NextDialogue => nextDialogue;
}
