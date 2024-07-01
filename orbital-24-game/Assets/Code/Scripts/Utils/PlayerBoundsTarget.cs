using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerBoundsTarget")]
public class PlayerBoundsTarget : ScriptableObject
{
    [SerializeField] private float boundHeight;
    public float BoundHeight => boundHeight;
    [SerializeField] private float boundWidth;
    public float BoundWidth => boundWidth;
    [SerializeField] private float boundOriginX;
    public float BoundOriginX => boundOriginX;
    [SerializeField] private float boundOriginY;
    public float BoundOriginY => boundOriginY;

}
