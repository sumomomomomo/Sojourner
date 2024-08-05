using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class OverworldMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject overworldMenuCanvas;
    [SerializeField] private BoolVariable isOverworldMenuOpen;
    [SerializeField] private BoolVariable isInventoryOpen;
    [SerializeField] private BoolVariable isStatusOpen;
    [SerializeField] private OverworldInputHandlerStateObject overworldInputHandlerStateObject;

    void Update()
    {
        if (overworldInputHandlerStateObject.CanOverworldMenuOpen() && !isOverworldMenuOpen.Value)
        {
            if (Input.GetKeyDown(KeyCode.I) || Keyboard.current[Key.I].wasPressedThisFrame)
            {
                isInventoryOpen.Value = false;
                isStatusOpen.Value = false;
                isOverworldMenuOpen.Value = !isOverworldMenuOpen.Value;
            }
        }
        else if (overworldInputHandlerStateObject.CanOverworldMenuClose() && isOverworldMenuOpen.Value)
        {
            if (Input.GetKeyDown(KeyCode.I) || Keyboard.current[Key.I].wasPressedThisFrame)
            {
                isOverworldMenuOpen.Value = !isOverworldMenuOpen.Value;
            }
        }

        if (isOverworldMenuOpen.Value)
        {
            overworldMenuCanvas.SetActive(true);
        }
        else
        {
            overworldMenuCanvas.SetActive(false);
        }
    }
}