using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class EnemyHealthWatcher : MonoBehaviour
{
    [SerializeField] private TMP_Text enemyHealthText;
    [SerializeField] private IntReference enemyHealth;
    [SerializeField] private EnemyLoadedTrackerObject enemyLoadedTrackerObject;
    void Update()
    {
        enemyHealthText.text = "Enemy: " + enemyHealth.Value + "/" + enemyLoadedTrackerObject.LoadedEnemy.MaxHP + " HP";
    }
}
