using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerBoundsTargetInstructions")]
public class BoundTargetInstructionsObject : ScriptableObject
{
    [SerializeField] private PlayerBoundsTarget playerBoundsTarget;
    public PlayerBoundsTarget PlayerBoundsTarget
    {
        get { return playerBoundsTarget; }
        set { playerBoundsTarget = value; }
    }

}
