using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinLoseHandler : MonoBehaviour
{
    [SerializeField] private GameEventObject onWin;
    [SerializeField] private GameEventObject onLose;

    public void OnLose()
    {
        throw new Exception("Unimplemented");
    }

    public void OnWin()
    {
        throw new Exception("Unimplemented");
    }
}
