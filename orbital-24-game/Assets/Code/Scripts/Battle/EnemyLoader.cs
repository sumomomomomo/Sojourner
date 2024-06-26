using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLoader : MonoBehaviour
{
    [SerializeField] private EnemyLoadedTrackerObject currentEnemy;
    //[SerializeField] private BoundTargetInstructionsObject boundTargetInstructionsObject;
    //[SerializeField] private PlayerBoundsTarget defaultPlayerBoundsTarget;
    void Start()
    {
        // Set player bounds
        // TODO put this init process into the enemy itself? For different init per enemy
        // TODO add functionality in defaultPlayerBoundsTarget to just update the bound target themselves
        //boundTargetInstructionsObject.PlayerBoundsTarget = defaultPlayerBoundsTarget;

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
