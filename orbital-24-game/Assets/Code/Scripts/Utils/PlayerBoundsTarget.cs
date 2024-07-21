using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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

    [InspectorButton("OnButtonClicked")] public bool Read;
    private void OnButtonClicked()
    {
        FloatVariable boundWidth = (FloatVariable) AssetDatabase.LoadAssetAtPath(
            AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("BoundWidth")[0]), 
            typeof(FloatVariable)
        );
        FloatVariable boundHeight = (FloatVariable) AssetDatabase.LoadAssetAtPath(
            AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("BoundHeight")[0]), 
            typeof(FloatVariable)
        );
        FloatVariable boundOriginXTranslation = (FloatVariable) AssetDatabase.LoadAssetAtPath(
            AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("BoundOriginXTranslation")[0]), 
            typeof(FloatVariable)
        );
        FloatVariable boundOriginYTranslation = (FloatVariable) AssetDatabase.LoadAssetAtPath(
            AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets("BoundOriginYTranslation")[0]), 
            typeof(FloatVariable)
        );
        this.boundWidth = boundWidth.Value;
        this.boundHeight = boundHeight.Value;
        this.boundOriginX = boundOriginXTranslation.Value;
        this.boundOriginY = boundOriginYTranslation.Value;
    }

}
