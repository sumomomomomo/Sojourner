using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnBar : MonoBehaviour
{
    
    [SerializeField] private Slider slider;
    [SerializeField] private FloatReference timeLeftForTurn;
    [SerializeField] private FloatReference timeLeftForTurnMax;
    [SerializeField] private BattleState battleState;

    [SerializeField] private GameObject turnBarText;

    private void Update()
    {
        if (battleState.IsPlayerTurn())
        {
            turnBarText.SetActive(true);
            slider.maxValue = timeLeftForTurnMax.Value;
            slider.value = timeLeftForTurn.Value;
        }
        else
        {
            turnBarText.SetActive(false);
            slider.maxValue = timeLeftForTurnMax.Value;
            slider.value = timeLeftForTurnMax.Value - timeLeftForTurn.Value;
        }
    }

}
