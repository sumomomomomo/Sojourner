using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusInputHandler : MonoBehaviour
{
    [SerializeField] private BoolVariable isStatusOpen;
    [SerializeField] private float timeSinceOpened;
    void Start()
    {
        timeSinceOpened = 0;
    }
    void Update()
    {
        if (!CanCloseStatus())
        {
            timeSinceOpened += Time.deltaTime;
        }
        if (isStatusOpen.Value && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)))
        {
            isStatusOpen.Value = false;
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

    private bool CanCloseStatus()
    {
        return timeSinceOpened > 0.1;
    }
}
