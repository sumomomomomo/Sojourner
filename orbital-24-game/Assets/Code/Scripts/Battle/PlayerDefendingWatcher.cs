using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlayerDefendingWatcher : MonoBehaviour
{
    [SerializeField] private BattleState battleState;
    [SerializeField] private TMP_Text hpText;
    [SerializeField] private TMP_Text hpValueText;
    [SerializeField] private Image hpBarFillImage;
    [SerializeField] private Color defendingColor;

    private void Update()
    {
        if (battleState.IsPlayerDefending())
        {
            hpText.color = defendingColor;
            hpValueText.color = defendingColor;
            hpBarFillImage.color = defendingColor;
        }
        else
        {
            hpText.color = Color.white;
            hpValueText.color = Color.white;
            hpBarFillImage.color = Color.white;
        }
    }
}
