using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInvulnHandler : MonoBehaviour
{
    [SerializeField] private FloatReference timeLeftForPlayerInvuln;
    [SerializeField] private FloatReference invulnTimeWhenHit;
    [SerializeField] private BattleState battleState;

    void Update()
    {
        if (battleState.IsPlayerInvulnerable())
        {
            timeLeftForPlayerInvuln.Value -= Time.deltaTime;
            if (timeLeftForPlayerInvuln.Value <= 0)
            {
                battleState.SetPlayerInvulnerable(false);
                timeLeftForPlayerInvuln.Value = 0;
            }
        }
    }

    public void EnableOnHitInvuln()
    {
        battleState.SetPlayerInvulnerable(true);
        timeLeftForPlayerInvuln.Value = invulnTimeWhenHit.Value;
    }
}
