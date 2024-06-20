using System.Collections;
using System.Collections.Generic;
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
}
