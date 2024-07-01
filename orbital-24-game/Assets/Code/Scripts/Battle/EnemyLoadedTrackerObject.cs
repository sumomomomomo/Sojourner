using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle/EnemyObjectLoadedTracker")]
public class EnemyLoadedTrackerObject : ScriptableObject
{
    [SerializeField] private EnemyObject loadedEnemy;
    public EnemyObject LoadedEnemy => loadedEnemy;
    [SerializeField] [TextArea] private string developerComments;
    public void LoadEnemy(EnemyObject enemyObject)
    {
        loadedEnemy = enemyObject;
    }
}
