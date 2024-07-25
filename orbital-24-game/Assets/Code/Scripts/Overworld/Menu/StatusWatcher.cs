using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusWatcher : MonoBehaviour
{
    [SerializeField] private BoolVariable isStatusOpen;
    [SerializeField] private GameObject statusBox;
    void Update()
    {
        if (isStatusOpen.Value)
        {
            statusBox.SetActive(true);
        }
        else
        {
            statusBox.SetActive(false);
        }
    }
}
