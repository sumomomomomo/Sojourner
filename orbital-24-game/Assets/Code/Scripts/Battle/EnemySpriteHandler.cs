using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpriteHandler : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private StringVariable enemyEmotion;
    private Dictionary<string, Sprite> stringToSprite = null;
    public void ChangeSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }

    private void Update()
    {
        if (stringToSprite != null && stringToSprite.ContainsKey(enemyEmotion.Value))
        {
            ChangeSprite(stringToSprite[enemyEmotion.Value]);
        }
    }

    public void SetStringToSprite(Dictionary<string, Sprite> d)
    {
        stringToSprite = d;
    }
}
