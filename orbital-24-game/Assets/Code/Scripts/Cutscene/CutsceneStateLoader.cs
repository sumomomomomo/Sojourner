using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CutsceneStateLoader : MonoBehaviour
{
    [SerializeField] private CutsceneEventSequenceObject cutsceneEventSequenceObject;
    [SerializeField] private UnityEvent onCutsceneIsCompleted;
    [SerializeField] [TextArea] string developerComments;
    void Start()
    {
        if (cutsceneEventSequenceObject.IsCutsceneFinished)
        {
            onCutsceneIsCompleted.Invoke();
        }
    }
}
