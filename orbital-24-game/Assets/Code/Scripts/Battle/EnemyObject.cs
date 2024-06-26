using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle/EnemyObject")]
public class EnemyObject : ScriptableObject
{
    [SerializeField] private string enemyName;
    public string EnemyName => enemyName;
    [SerializeField] private int maxHP;
    public int MaxHP => maxHP;
    [SerializeField] private GameObject enemyHandlerPrefab;
    public GameObject EnemyHandlerPrefab => enemyHandlerPrefab;
    [SerializeField] private EnemyLoadedTrackerObject enemyLoadedTrackerObject;
    [SerializeField] private bool hasEnemyLoadedTrackerObject = false;
    [SerializeField] [TextArea] private string developerComments;

    #if UNITY_EDITOR
    public void Awake()
    {
        if (!hasEnemyLoadedTrackerObject)
        {
            string[] guids = AssetDatabase.FindAssets("t:EnemyLoadedTrackerObject");
            if (guids.Length < 1)
            {
                Debug.LogError("Cannot find EnemyLoadedTrackerObject");
            }
            enemyLoadedTrackerObject = (EnemyLoadedTrackerObject) AssetDatabase.LoadAssetAtPath(
                AssetDatabase.GUIDToAssetPath(guids[0]), typeof(EnemyLoadedTrackerObject));
            hasEnemyLoadedTrackerObject = true;
        }
    }
    #endif

    public void SetEnemyLoadedTrackerObjectToThis()
    {
        if (!hasEnemyLoadedTrackerObject)
        {
            Debug.LogError("No EnemyLoadedTrackerObject set!");
        }
        else
        {
            enemyLoadedTrackerObject.LoadEnemy(this);
        }
    }
}
