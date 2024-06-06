using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Int")]
public class IntVariable : ScriptableObject
{
    [SerializeField] private int value;
    [SerializeField] [TextArea] private string developerComments;

    public int Value { get => value; set => this.value = value; }
}
