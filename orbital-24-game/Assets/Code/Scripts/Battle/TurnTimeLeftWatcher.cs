using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TurnTimeLeftWatcher : MonoBehaviour
{
    [SerializeField] private TMP_Text countdownTimer;
    [SerializeField] private FloatReference timeLeftToNextTurn;
    [SerializeField] private BoolReference isPlayerTurn;
    void Update()
    {
        countdownTimer.text = ((int) Math.Floor(timeLeftToNextTurn.Value)).ToString() + "s (" + (isPlayerTurn.Value ? "Player" : "Enemy") + ")";
    }
}
