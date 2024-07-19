using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/Sprite Behaviour")]
public class DialogueSpriteBehaviour : ScriptableObject
{
    [SerializeField] private string label;
    [SerializeField] private float leftXCoordinate;
    [SerializeField] private float leftYCoordinate;
    [SerializeField] private float rightXCoordinate;
    [SerializeField] private float rightYCoordinate;
    public float LeftXCoordinate => leftXCoordinate;
    public float LeftYCoordinate => leftYCoordinate;
    public float RightXCoordinate => rightXCoordinate;
    public float RightYCoordinate => rightYCoordinate;
}
