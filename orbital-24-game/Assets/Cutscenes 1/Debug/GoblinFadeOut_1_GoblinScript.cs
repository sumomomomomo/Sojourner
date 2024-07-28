using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class GoblinFadeOut_1_GoblinScript : MonoBehaviour
{
    [SerializeField] private CutsceneEventSequenceObject cutsceneEventSequenceObject;
    public void EndCutscene()
    {
        cutsceneEventSequenceObject.StopCutsceneSequence();
    }
}
