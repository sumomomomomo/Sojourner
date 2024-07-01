using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Bool")]
public class BoolVariable : ScriptableObject
{
    [SerializeField] private bool value;
    [SerializeField] [TextArea] private string developerComments;

    public bool Value { get => value; set => this.value = value; }
}
