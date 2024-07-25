using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryInputHandler : MonoBehaviour
{
    [SerializeField] private BoolVariable isInventoryOpen;
    [SerializeField] private float timeSinceOpened;
    void Start()
    {
        timeSinceOpened = 0;
    }
    void Update()
    {
        if (!CanCloseInventory())
        {
            timeSinceOpened += Time.deltaTime;
        }
        else if (isInventoryOpen.Value && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
        {
            isInventoryOpen.Value = false;
        }
    }
    void OnEnable()
    {
        timeSinceOpened = 0;
    }

    void OnDisable()
    {
        timeSinceOpened = 0;
    }

    private bool CanCloseInventory()
    {
        return timeSinceOpened > 0.1;
    }
}
