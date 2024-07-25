using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryWatcher : MonoBehaviour
{
    [SerializeField] private BoolVariable isInventoryOpen;
    [SerializeField] private GameObject statusBox;
    void Update()
    {
        if (isInventoryOpen.Value)
        {
            statusBox.SetActive(true);
        }
        else
        {
            statusBox.SetActive(false);
        }

    }
}
