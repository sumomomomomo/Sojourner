using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    
    [SerializeField] private Slider slider;
    [SerializeField] private IntReference playerHP;
    [SerializeField] private IntReference playerMaxHP;

    private void Update()
    {
        UpdateHealth(playerHP.Value, playerMaxHP.Value, slider);
    }

    private void UpdateHealth(int playerHP, int playerMaxHP, Slider slider)
    {
        slider.maxValue = playerMaxHP;
        slider.value = playerHP;
    }

}
