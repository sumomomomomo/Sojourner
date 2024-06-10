using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHealthWatcher : MonoBehaviour
{
    [SerializeField] private TMP_Text enemyHealthText;
    [SerializeField] private IntReference enemyHealth;
    [SerializeField] private IntReference enemyMaxHealth;
    void Update()
    {
        enemyHealthText.text = "Enemy: " + enemyHealth.Value + "/" + enemyMaxHealth.Value + " HP";
    }
}
