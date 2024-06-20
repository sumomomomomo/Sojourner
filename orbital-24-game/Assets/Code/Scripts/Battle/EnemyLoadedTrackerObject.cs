using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle/EnemyObjectLoadedTracker")]
public class EnemyLoadedTrackerObject : ScriptableObject
{
    [SerializeField] private EnemyObject loadedEnemy;
    public EnemyObject LoadedEnemy => loadedEnemy;
}
