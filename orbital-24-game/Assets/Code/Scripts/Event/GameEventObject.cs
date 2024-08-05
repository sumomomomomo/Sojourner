using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEventObject : ScriptableObject
{
    [SerializeField] [TextArea] private string developerComments;
    private readonly List<GameEventListener> listeners = new();

    public void Raise()
    {
        for (int i = listeners.Count - 1; i >= 0; i--) 
        {
            listeners[i].OnEventRaised();
        }
    }

    public void RegisterListener(GameEventListener listener)
    { 
        listeners.Add(listener); 
    }

    public void UnregisterListener(GameEventListener listener)
    { 
        listeners.Remove(listener); 
    }
}
