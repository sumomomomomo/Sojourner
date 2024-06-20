using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnEndMover : MonoBehaviour
{
    [SerializeField] private GameObject playerGroup;
    [SerializeField] private BoundTargetInstructionsObject boundTargetInstructionsObject;

    public void MovePlayerToCenterOfTarget()
    {
        playerGroup.transform.position = new Vector2(boundTargetInstructionsObject.PlayerBoundsTarget.BoundOriginX, boundTargetInstructionsObject.PlayerBoundsTarget.BoundOriginY);
    }
}
