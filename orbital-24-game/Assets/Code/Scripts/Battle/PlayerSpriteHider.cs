using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpriteHider : MonoBehaviour
{
    [SerializeField] private SpriteRenderer playerSpriteRenderer;
    [SerializeField] private BoolVariable isFreezeTurn;
    void Update()
    {
        if (isFreezeTurn.Value) playerSpriteRenderer.enabled = false;
        else playerSpriteRenderer.enabled = true;
    }
}
