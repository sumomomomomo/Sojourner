using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cutscenes/Event Object")]
public class CutsceneEventObject : ScriptableObject
{
    [SerializeField] [TextArea] private string developerComments;
    private readonly List<CutsceneEventListener> listeners = new();
    public void StartCutscene()
    {
        for (int i = listeners.Count - 1; i >= 0; i--) 
        {
            listeners[i].OnStartCutscene();
        }
    }

    public void RegisterListener(CutsceneEventListener listener)
    { 
        listeners.Add(listener); 
    }

    public void UnregisterListener(CutsceneEventListener listener)
    { 
        listeners.Remove(listener);
    }
}
