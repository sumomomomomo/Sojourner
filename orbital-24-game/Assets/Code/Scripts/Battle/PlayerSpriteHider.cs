using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteHider : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private BoolVariable isFreezeTurn;
    [SerializeField] private BoolVariable isBattleWin;
    [SerializeField] private BoolVariable isBattleLose;
    void Update()
    {
        if (isFreezeTurn.Value || isBattleLose.Value || isBattleWin.Value) 
        {
            playerSpriteRenderer.enabled = false;
        }
        else 
        {
            playerSpriteRenderer.enabled = true;
        }
    }
}
