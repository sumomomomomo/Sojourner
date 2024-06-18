using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInvulnWatcher : MonoBehaviour
{
    private Animator playerAnimator;
    [SerializeField] private TMP_Text invulnTimer;
    [SerializeField] private FloatReference timeLeftForInvuln;
    [SerializeField] private BoolReference isInvuln;

    void Start()
    {
        playerAnimator = GetComponentInParent<Animator>();
    }
    void Update()
    {
        if (isInvuln.Value)
        {
            invulnTimer.text = "Invuln for " + ((int) Math.Floor(timeLeftForInvuln.Value)).ToString() + "s";
            if (playerAnimator != null) playerAnimator.Play("Invuln Blink");
        }
        else
        {
            invulnTimer.text = "";
            if (playerAnimator != null) playerAnimator.Play("Normal");
        }
    }
}
