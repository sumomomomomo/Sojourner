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
    [SerializeField] private int def;
    public int Def => def;
    [SerializeField] private int atk;
    public int Atk => atk;
    [SerializeField] private EnemyLoadedTrackerObject enemyLoadedTrackerObject;
    [SerializeField] private bool hasEnemyLoadedTrackerObject = false;
    [SerializeField] private Object _enemyHandlerState;
    public IEnemyHandlerState EnemyHandlerState => (IEnemyHandlerState) _enemyHandlerState;
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
