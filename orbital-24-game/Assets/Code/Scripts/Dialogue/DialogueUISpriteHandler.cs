using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class DialogueUISpriteHandler : MonoBehaviour
{
    [SerializeField] private Image leftSpriteImage;
    [SerializeField] private Image rightSpriteImage;
    [SerializeField] private RectTransform leftSpriteContainer;
    [SerializeField] private RectTransform rightSpriteContainer;

    private void Start()
    {
        HideSprites();
    }

    public void ShowSprites(DialogueSpritePair dialogueSpritePair)
    {
        leftSpriteImage.enabled = true;
        rightSpriteImage.enabled = true;
        leftSpriteImage.sprite = dialogueSpritePair.LeftSprite;
        rightSpriteImage.sprite = dialogueSpritePair.RightSprite;


        // Load specific sprite behaviour
        leftSpriteContainer.anchoredPosition = new Vector2(
            dialogueSpritePair.SpriteBehaviour.LeftXCoordinate, 
            dialogueSpritePair.SpriteBehaviour.LeftYCoordinate
        );
        rightSpriteContainer.anchoredPosition = new Vector2(
            dialogueSpritePair.SpriteBehaviour.RightXCoordinate,
            dialogueSpritePair.SpriteBehaviour.RightYCoordinate
        );
    }

    public void HideSprites()
    {
        leftSpriteImage.enabled = false;
        rightSpriteImage.enabled = false;
    }
}
