using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameEventObject))]
public class GameEventObjectInspectorAddRaise : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GameEventObject myGameEvent = (GameEventObject) target;
        if (GUILayout.Button("Raise()"))
        {
            myGameEvent.Raise();
        }
    }
}