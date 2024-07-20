using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    private EnemyObject enemyObject;

    public void SetEnemyObject(EnemyObject newEnemyObject)
    {
        enemyObject = newEnemyObject;
    }

    public void Hide()
    {
        slider.gameObject.SetActive(false);
    }

    public void Show()
    {
        slider.gameObject.SetActive(true);
    }

    private void Update()
    {
        slider.maxValue = enemyObject.MaxHP;
        slider.value = enemyObject.CurrHP;
    }
}
