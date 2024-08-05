using UnityEngine;
using UnityEngine.Events;

public class CutsceneEventListener : MonoBehaviour
{
    [SerializeField] private CutsceneEventObject Event;
    [SerializeField] private UnityEvent Response;
    [SerializeField] [TextArea] private string developerComments;

    private void OnEnable()
    { 
        Event.RegisterListener(this); 
    }

    private void OnDisable()
    { 
        Event.UnregisterListener(this); 
    }

    public void OnStartCutscene()
    { 
        Response.Invoke();
    }
}