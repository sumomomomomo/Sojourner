using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryClose : MonoBehaviour
{
   [SerializeField] private GameObject inventoryCanvas;

    void Start()
    {
        inventoryCanvas.SetActive(false);
    }
}