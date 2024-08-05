using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CutsceneEventObject))]
public class CutsceneEventObjectInspectorAddRaise : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        CutsceneEventObject myGameEvent = (CutsceneEventObject) target;
        if (GUILayout.Button("StartCutscene()"))
        {
            myGameEvent.StartCutscene();
        }
    }
}