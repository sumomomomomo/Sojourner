using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour, IEnemyHandler
{
    [SerializeField] private GameObject knifeAttackPrefab;
    private readonly List<GameObject> instantiatedObjects = new();
    public void onEnemyTurnEnd()
    {
        for (int i = 0; i < instantiatedObjects.Count; i++)
        {
            Destroy(instantiatedObjects[i]);
        }
    }

    public void onEnemyTurnStart()
    {
        instantiatedObjects.Add(Instantiate(knifeAttackPrefab));
    }
}
