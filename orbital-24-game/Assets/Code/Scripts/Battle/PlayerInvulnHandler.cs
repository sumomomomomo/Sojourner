using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvulnHandler : MonoBehaviour
{
    [SerializeField] private FloatReference timeLeftForPlayerInvuln;
    [SerializeField] private FloatReference invulnTimeWhenHit;
    [SerializeField] private BoolReference isPlayerInvuln;

    void Update()
    {
        if (isPlayerInvuln.Value)
        {
            timeLeftForPlayerInvuln.Value -= Time.deltaTime;
            if (timeLeftForPlayerInvuln.Value <= 0)
            {
                isPlayerInvuln.Value = false;
                timeLeftForPlayerInvuln.Value = 0;
            }
        }
    }

    public void EnableOnHitInvuln()
    {
        isPlayerInvuln.Value = true;
        timeLeftForPlayerInvuln.Value = invulnTimeWhenHit.Value;
    }
}
