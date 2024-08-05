using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variable/Float")]
public class FloatVariable : ScriptableObject
{
    [SerializeField] private float value;
    [SerializeField] [TextArea] private string developerComments;

    public float Value { get => value; set => this.value = value; }

    private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;
}
