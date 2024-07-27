using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnDefendHealer : MonoBehaviour
{
    [SerializeField] private IntVariable playerHP;
    [SerializeField] private IntVariable playerMaxHP;
    [SerializeField] private IntVariable healAmt;

    public void DefendHeal()
    {
        playerHP.Value += healAmt.Value;
        playerHP.Value = Mathf.Min(playerMaxHP.Value, playerHP.Value);
    }
}
