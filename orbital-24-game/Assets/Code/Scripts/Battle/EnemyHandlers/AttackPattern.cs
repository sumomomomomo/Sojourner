using System;
using UnityEngine;

[Serializable]
public class AttackPattern
{
    [SerializeField] private GameObject attackPrefab;
    public GameObject AttackPrefab => attackPrefab;
    [SerializeField] private PlayerBoundsTarget playerBoundsTarget;
    public PlayerBoundsTarget PlayerBoundsTarget => playerBoundsTarget;
}
