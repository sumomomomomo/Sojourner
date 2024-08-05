using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Text damageText;
    [SerializeField] private Animator animator;
    private EnemyObject enemyObject;

    public void SetEnemyObject(EnemyObject newEnemyObject)
    {
        enemyObject = newEnemyObject;
    }

    public void Hide()
    {
        slider.gameObject.SetActive(false);
        damageText.enabled = false;
    }

    public void Show()
    {
        slider.gameObject.SetActive(true);
    }

    public void ShowWithDamageTextJump(int damage)
    {
        slider.gameObject.SetActive(true);
        damageText.text = damage.ToString();
        damageText.enabled = true;

        animator.Play("Damage Jump");
    }

    private void Update()
    {
        slider.maxValue = enemyObject.MaxHP;
        slider.value = enemyObject.CurrHP;
    }
}
