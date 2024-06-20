using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : MonoBehaviour, IEnemyHandler
{
    [SerializeField] private AttackPattern[] attackPatterns;
    [SerializeField] private BoundTargetInstructionsObject boundTargetInstructionsObject;
    [SerializeField] private GameEventObject onUpdateBounds;
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
        AttackPattern chosenAttack = attackPatterns[0];
        boundTargetInstructionsObject.PlayerBoundsTarget = chosenAttack.PlayerBoundsTarget;
        onUpdateBounds.Raise();
        instantiatedObjects.Add(Instantiate(chosenAttack.AttackPrefab));
    }
}
