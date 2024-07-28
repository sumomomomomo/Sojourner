using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnSightTriggerCutscene : MonoBehaviour
{
    [SerializeField] CutsceneEventSequenceObject cutsceneEventSequenceObject;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.TryGetComponent(out Player player))
        {
            if (!cutsceneEventSequenceObject.IsCutsceneFinished)
                cutsceneEventSequenceObject.StartCutsceneSequence();
        }
    }
}
