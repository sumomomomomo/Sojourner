using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Handler States/Goblin")]
public class GoblinHandlerState : ScriptableObject, IEnemyHandlerState
{
    [SerializeField] private AttackPattern[] attackPatterns;
    [SerializeField] private BoundTargetInstructionsObject boundTargetInstructionsObject;
    [SerializeField] private GameEventObject onUpdateBounds;
    [SerializeField] private BoolReference isFreezeTurn;
    private List<GameObject> instantiatedObjects = new();
    public void OnEnemyTurnEnd(MonoBehaviour monoBehaviour)
    {
        for (int i = 0; i < instantiatedObjects.Count; i++)
        {
            Destroy(instantiatedObjects[i]);
        }
        instantiatedObjects = new();
    }

    public void OnEnemyTurnStart(MonoBehaviour monoBehaviour)
    {
        AttackPattern chosenAttack = attackPatterns[Random.Range(0,2)];
        boundTargetInstructionsObject.PlayerBoundsTarget = chosenAttack.PlayerBoundsTarget;
        onUpdateBounds.Raise();
        monoBehaviour.StartCoroutine(DoAttack(chosenAttack));
    }

    private IEnumerator DoAttack(AttackPattern attackPattern)
    {
        yield return new WaitForSeconds(0.25f);
        while (isFreezeTurn.Value)
        {
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.25f);
        instantiatedObjects.Add(Instantiate(attackPattern.AttackPrefab));
    }
}
