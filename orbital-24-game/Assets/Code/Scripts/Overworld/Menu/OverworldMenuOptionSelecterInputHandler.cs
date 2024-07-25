using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldMenuOptionSelecterInputHandler : MonoBehaviour
{
    [SerializeField] private OverworldMenuOptionTracker overworldMenuOptionTracker;
    [SerializeField] private OverworldInputHandlerStateObject overworldInputHandlerStateObject;

    void Start()
    {
        overworldMenuOptionTracker.InitDefaultOption();
    }
    void Update()
    {
        if (overworldInputHandlerStateObject.IsMenuOptionsSelectable())
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                overworldMenuOptionTracker.ToPreviousOption();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                overworldMenuOptionTracker.ToNextOption();
            }
            else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
            {
                overworldMenuOptionTracker.SelectCurentOption().Invoke();
            }
        }
    }
}
