using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoader : MonoBehaviour
{
    [SerializeField] private EnemyLoadedTrackerObject currentEnemy;
    void Start()
    {
        if (currentEnemy != null) 
        {
            if (currentEnemy.LoadedEnemy.EnemyHandlerPrefab == null)
            {
                Debug.Log("No enemy handler");
                return;
            }
            Instantiate(currentEnemy.LoadedEnemy.EnemyHandlerPrefab);
            Debug.Log("Enemy handler loaded for " + currentEnemy.LoadedEnemy.EnemyName);
        }
        else
        {
            Debug.Log("Enemy not set");    
        }
    }
}
