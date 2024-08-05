using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackloggedCutsceneSequenceHandler : MonoBehaviour
{
    [SerializeField] private BackloggedCutsceneSequenceObject backloggedCutsceneSequenceObject;
    void Start()
    {
        backloggedCutsceneSequenceObject.RaiseAndRemoveEventSequence();
    }
}
