using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInvulnWatcher : MonoBehaviour
{
    [SerializeField] private TMP_Text invulnTimer;
    [SerializeField] private FloatReference timeLeftForInvuln;
    [SerializeField] private BoolReference isInvuln;
    void Update()
    {
        if (isInvuln.Value)
        {
            invulnTimer.text = "Invuln for " + ((int) Math.Floor(timeLeftForInvuln.Value)).ToString() + "s";
        }
        else
        {
            invulnTimer.text = "";
        }
    }
}
