using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle/EnemyObject")]
public class EnemyObject : ScriptableObject
{
    [SerializeField] private int maxHP;
    public int MaxHP => maxHP;

    // TODO enemy sprite, agility, attack patterns
}
