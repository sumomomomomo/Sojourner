using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinFadeOut_StateLoader : MonoBehaviour
{
    [SerializeField] private CutsceneEventSequenceObject cutsceneEventSequenceObject;
    [SerializeField] private GameObject goblin;
    [SerializeField] [TextArea] private string developerComments;
    void Start()
    {
        if (cutsceneEventSequenceObject.IsCutsceneFinished)
        {
            goblin.SetActive(false);
        }
    }
}
