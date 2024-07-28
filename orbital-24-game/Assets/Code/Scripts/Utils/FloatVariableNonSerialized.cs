using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Float NS")]
public class FloatVariableNonSerialized : ScriptableObject
{
    public float value;
    [SerializeField] [TextArea] private string developerComments;

    public float Value { get => value; set => this.value = value; }
}
