using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInvulnWatcher : MonoBehaviour
{
    private Animator playerAnimator;
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
            if (playerAnimator != null) playerAnimator.Play("Invuln Blink");
        }
        else
        {
            if (playerAnimator != null) playerAnimator.Play("Normal");
        }
    }
}
