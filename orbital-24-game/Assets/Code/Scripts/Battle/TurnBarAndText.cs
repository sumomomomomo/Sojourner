using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnBarAndText : MonoBehaviour
{
    
    [SerializeField] private Slider slider;
    [SerializeField] private FloatReference timeLeftForTurn;
    [SerializeField] private FloatReference timeLeftForTurnMax;
    [SerializeField] private BattleState battleState;

    [SerializeField] private TMP_Text turnBarText;
    [SerializeField] private Animator turnBarTextAnimator;

    private void Update()
    {
        if (battleState.IsPlayerTurn())
        {
            if (battleState.IsPlayerTalking())
            {
                turnBarText.text = "TALK!";
            }
            else
            {
                turnBarText.text = "ACT!";
            }
            turnBarText.gameObject.SetActive(true);
            if (timeLeftForTurn.Value <= timeLeftForTurnMax.Value / 2)
            {
                turnBarTextAnimator.Play("Act Blink");
            }
            else
            {
                turnBarTextAnimator.Play("Default");
            }
            slider.maxValue = timeLeftForTurnMax.Value;
            slider.value = timeLeftForTurn.Value;
        }
        else
        {
            turnBarText.gameObject.SetActive(false);
            slider.maxValue = timeLeftForTurnMax.Value;
            slider.value = timeLeftForTurnMax.Value - timeLeftForTurn.Value;
        }
    }

}
