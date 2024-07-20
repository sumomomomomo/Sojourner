using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/String")]
public class StringVariable : ScriptableObject
{
    [SerializeField] private string value;
    [SerializeField] [TextArea] private string developerComments;

    public string Value { get => value; set => this.value = value; }
}
