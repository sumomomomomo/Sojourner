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
        slider.maxValue = playerMaxHP.Value; 
        slider.value = playerHP.Value;
    }

}
