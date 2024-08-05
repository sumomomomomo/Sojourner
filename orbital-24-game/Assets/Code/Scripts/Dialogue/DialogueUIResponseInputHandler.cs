using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DialogueUIResponseInputHandler : MonoBehaviour
{
    private List<UnityAction> unityActions;
    private IntVariable selectedActionIndex;
    private bool isInit = false;
    private void Update()
    {
        if (isInit)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Keyboard.current[Key.Space].wasPressedThisFrame)
            {
                if (selectedActionIndex.Value < 0 || selectedActionIndex.Value >= unityActions.Count)
                {
                    Debug.Log("Invalid response index");
                }
                else
                {
                    unityActions[selectedActionIndex.Value].Invoke();
                }
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || 
            Keyboard.current[Key.UpArrow].wasPressedThisFrame || Keyboard.current[Key.W].wasPressedThisFrame)
            {
                selectedActionIndex.Value -= 1;
                selectedActionIndex.Value = Mathf.Clamp(selectedActionIndex.Value, 0, unityActions.Count - 1);
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || 
            Keyboard.current[Key.DownArrow].wasPressedThisFrame || Keyboard.current[Key.S].wasPressedThisFrame)
            {
                selectedActionIndex.Value += 1;
                selectedActionIndex.Value = Mathf.Clamp(selectedActionIndex.Value, 0, unityActions.Count - 1);
            }
        }
    }

    public void Init(List<UnityAction> newUnityActions, IntVariable newSelectedActionIndex)
    {
        unityActions = newUnityActions;
        selectedActionIndex = newSelectedActionIndex;
        isInit = true;
    }
}
