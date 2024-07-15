using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cutscenes/Backlogged Cutscene Sequence")]
public class BackloggedCutsceneSequenceObject : ScriptableObject
{
    [SerializeField] private CutsceneEventSequenceObject cutsceneEventSequenceObject;
    [SerializeField] [TextArea] private string developerComments;

    public void LoadCutsceneEventSequence(CutsceneEventSequenceObject newObj)
    {
        cutsceneEventSequenceObject = newObj;
    }

    public void RaiseAndRemoveEventSequence()
    {
        if (cutsceneEventSequenceObject == null)
        {
            Debug.Log("No cutscene in backlog");
            return;
        }
        cutsceneEventSequenceObject.StartCutsceneSequence();
        RemoveEventSequence();
    }

    public void RemoveEventSequence()
    {
        cutsceneEventSequenceObject = null;
    }
}
