using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Data structure representing left/right sprites on overworld dialogue.
/// Compose within DialogueObject.
/// </summary>
[System.Serializable]
public class DialogueSpritePair
{
    [SerializeField] private DialogueSpriteBehaviour dialogueSpriteBehaviour;
    public DialogueSpriteBehaviour SpriteBehaviour => dialogueSpriteBehaviour;
    [SerializeField] private Sprite leftSprite;
    public Sprite LeftSprite => leftSprite;
    [SerializeField] private Sprite rightSprite;
    public Sprite RightSprite => rightSprite;
}
