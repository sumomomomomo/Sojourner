using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class OverworldMenuHandler : MonoBehaviour
{
    [SerializeField] private GameObject overworldMenuCanvas;
    [SerializeField] private BoolReference isOverworldMenuOpen;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            isOverworldMenuOpen.Value = !isOverworldMenuOpen.Value;
        }
        if (isOverworldMenuOpen.Value)
        {
            overworldMenuCanvas.SetActive(true);
        }
        if (!isOverworldMenuOpen.Value)
        {
            overworldMenuCanvas.SetActive(false);
        }
    }
}