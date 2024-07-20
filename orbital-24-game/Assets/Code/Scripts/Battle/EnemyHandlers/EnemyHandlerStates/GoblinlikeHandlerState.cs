using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Enemy Handler States/Goblinlike")]
public class GoblinlikeHandlerState : ScriptableObject, IEnemyHandlerState
{
    [SerializeField] private AttackPattern[] attackPatterns;
    [SerializeField] private BoundTargetInstructionsObject boundTargetInstructionsObject;
    [SerializeField] private GameEventObject onUpdateBounds;
    [SerializeField] private BoolReference isFreezeTurn;
    [SerializeField] private EnemyObject enemyObject;
    [SerializeField] private GameObject winTextPrefab;
    private List<GameObject> instantiatedObjects = new();
    private GameObject spriteObject;
    private GameObject healthBarSliderCanvasObject;
    private EnemyHealthBar enemyHealthBar;
    public void OnBattleStart(MonoBehaviour monoBehaviour)
    {
        // instantiate enemy sprite
        spriteObject = Instantiate(enemyObject.SpritePrefab);

        healthBarSliderCanvasObject = Instantiate(enemyObject.HealthBarPrefab);
        enemyHealthBar = healthBarSliderCanvasObject.GetComponentInChildren<EnemyHealthBar>();
        enemyHealthBar.SetEnemyObject(enemyObject);
        enemyHealthBar.Hide();
    }
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
        boundTargetInstructionsObject.PlayerBoundsTarget = chosenAttack.PlayerBoundsTarget; // TODO redo?
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

    public void OnPlayerWin(MonoBehaviour monoBehaviour)
    {
        // sprite fades away
        Destroy(spriteObject);
        Destroy(healthBarSliderCanvasObject);
        // wintext appears
        Instantiate(winTextPrefab);
    }

    public void OnTakeDamage(MonoBehaviour monoBehaviour, IntVariable enemyHP, BattleState battleState)
    {
        battleState.SetEnemyDamageAnimationPlaying(true);
        monoBehaviour.StartCoroutine(AnimateDamageTaken(enemyHP, battleState));
    }

    private IEnumerator AnimateDamageTaken(IntVariable enemyHP, BattleState battleState)
    {
        float beforeHP = enemyObject.CurrHP;
        float afterHP = enemyHP.Value;
        enemyHealthBar.ShowWithDamageTextJump((int) (beforeHP - afterHP));
        yield return CoroutineUtils.Lerp(battleState.PlayerDamageAnimationLength, t => {
            enemyObject.SetCurrHP(Mathf.Lerp(beforeHP, afterHP, t));
        });
        enemyObject.SetCurrHP(enemyHP.Value);

        yield return new WaitForSeconds(1f);

        battleState.SetEnemyDamageAnimationPlaying(false);
        enemyHealthBar.Hide();
    }
}
