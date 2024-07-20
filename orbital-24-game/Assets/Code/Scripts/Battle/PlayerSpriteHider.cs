using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteHider : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private BattleState battleState;
    void Update()
    {
        if (battleState.IsPlayerHidden()) 
        {
            playerSpriteRenderer.enabled = false;
        }
        else 
        {
            playerSpriteRenderer.enabled = true;
        }
    }
}
