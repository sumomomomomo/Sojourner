using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OverworldMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject overworldMenuCanvas;
    [SerializeField] private BoolReference isOverworldMenuOpen;
    [SerializeField] private OverworldInputHandlerStateObject overworldInputHandlerStateObject;

    void Update()
    {
        if (overworldInputHandlerStateObject.CanOverworldMenuOpen() && !isOverworldMenuOpen.Value)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                isOverworldMenuOpen.Value = !isOverworldMenuOpen.Value;
            }
        }
        else if (overworldInputHandlerStateObject.CanOverworldMenuClose() && isOverworldMenuOpen.Value)
        {
            if (Input.GetKeyDown(KeyCode.I))
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