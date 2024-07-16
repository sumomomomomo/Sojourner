using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CutsceneEventSequenceObject))]
public class CutsceneEventSequenceObjectInspectorAddRaise : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        CutsceneEventSequenceObject myGameEvent = (CutsceneEventSequenceObject) target;
        if (GUILayout.Button("StartCutsceneSequence()"))
        {
            myGameEvent.StartCutsceneSequence();
        }
    }
}