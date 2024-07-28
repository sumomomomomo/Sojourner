using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Playables;

[CreateAssetMenu(menuName = "Cutscenes/Sequence")]
public class CutsceneEventSequenceObject : ScriptableObject
{
    [SerializeField] private BoolVariable isCutscenePlayingOverworld; // TODO only for overworld cutscenes
    [SerializeField] private List<CutsceneEventObject> cutsceneEventObjects;
    [SerializeField] private bool isCutsceneFinished = false;
    public bool IsCutsceneFinished => isCutsceneFinished;
    [SerializeField] [TextArea] private string developerComments;
    private int currIndex;

    #if UNITY_EDITOR
    public void Awake()
    {
        if (isCutscenePlayingOverworld == null)
        {
            string[] guids = AssetDatabase.FindAssets("IsCutscenePlayingOverworld");
            if (guids.Length < 1)
            {
                Debug.LogError("Cannot find IsCutscenePlayingOverworld");
            }
            isCutscenePlayingOverworld = (BoolVariable) AssetDatabase.LoadAssetAtPath(
                AssetDatabase.GUIDToAssetPath(guids[0]), typeof(BoolVariable));
        }
    }
    #endif
    public void StartCutsceneSequence()
    {
        if (cutsceneEventObjects == null || cutsceneEventObjects.Count < 1)
        {
            Debug.LogError("CutsceneEventSequence object empty!");
            return;
        }
        if (isCutscenePlayingOverworld.Value == true)
        {
            Debug.Log("Another cutscene is playing...");
            return;
        }
        currIndex = 0;
        isCutscenePlayingOverworld.Value = true;
        cutsceneEventObjects[currIndex].StartCutscene();
    }

    public void ContinueCutsceneSequence() // CutsceneEventListener should call this
    {
        currIndex += 1;
        if (cutsceneEventObjects.Count == currIndex)
        {
            StopCutsceneSequence();
        }
        else
        {
            // play next section
            cutsceneEventObjects[currIndex].StartCutscene();
        }
    }

    public void StopCutsceneSequence()
    {
        isCutscenePlayingOverworld.Value = false;
        isCutsceneFinished = true;
    }

    public void SetFinished(bool b)
    {
        isCutsceneFinished = b;
    }
    
}
