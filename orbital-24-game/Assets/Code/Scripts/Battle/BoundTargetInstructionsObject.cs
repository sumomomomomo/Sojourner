using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerBoundsTargetInstructions")]
public class BoundTargetInstructionsObject : ScriptableObject
{
    [SerializeField] private PlayerBoundsTarget playerBoundsTarget;
    [SerializeField] private GameEventObject onUpdateBounds;
    public PlayerBoundsTarget PlayerBoundsTarget
    {
        get { return playerBoundsTarget; }
        set { playerBoundsTarget = value; }
    }

    public void SetAndRaiseUpdateBounds(PlayerBoundsTarget playerBoundsTarget)
    {
        this.playerBoundsTarget = playerBoundsTarget;
        onUpdateBounds.Raise();
    }

}
