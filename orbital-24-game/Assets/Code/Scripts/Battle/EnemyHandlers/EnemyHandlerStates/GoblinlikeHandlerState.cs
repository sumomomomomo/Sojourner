using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [SerializeField] private StringVariable enemyEmotion;
    private List<GameObject> instantiatedObjects = new();
    private GameObject spriteObject;
    private GameObject healthBarSliderCanvasObject;
    private GameObject dialogueBoxObject;
    private EnemySpriteHandler enemySpriteHandler;
    private EnemyHealthBar enemyHealthBar;
    private EnemyDialogueHandler enemyDialogueHandler;
    public void OnBattleStart(MonoBehaviour monoBehaviour)
    {
        // instantiate enemy sprite
        spriteObject = Instantiate(enemyObject.SpritePrefab);
        enemySpriteHandler = spriteObject.GetComponentInChildren<EnemySpriteHandler>();
        enemySpriteHandler.SetStringToSprite(enemyObject.GetStringToSpriteDictionary());

        // instantiate enemy health bar
        healthBarSliderCanvasObject = Instantiate(enemyObject.HealthBarPrefab);
        enemyHealthBar = healthBarSliderCanvasObject.GetComponentInChildren<EnemyHealthBar>();
        enemyHealthBar.SetEnemyObject(enemyObject);
        enemyHealthBar.Hide();

        // instantiate enemy dialogue box
        dialogueBoxObject = Instantiate(enemyObject.DialogueBoxPrefab);
        enemyDialogueHandler = dialogueBoxObject.GetComponentInChildren<EnemyDialogueHandler>();
        enemyDialogueHandler.ClearText();
        enemyDialogueHandler.Hide();

        // instantiate emotion
        // TODO make this a StringVariable
        enemyEmotion.Value = "angry";
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
        AttackPattern chosenAttack = attackPatterns[UnityEngine.Random.Range(0,2)];
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
        Destroy(dialogueBoxObject);
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

        yield return new WaitForSeconds(battleState.PlayerDamageAnimationLength + 0.2f);

        enemyHealthBar.ShowWithDamageTextJump((int) (beforeHP - afterHP));
        yield return CoroutineUtils.Lerp(battleState.PlayerDamageAnimationLength, t => {
            enemyObject.SetCurrHP(Mathf.Lerp(beforeHP, afterHP, t));
        });
        enemyObject.SetCurrHP(enemyHP.Value);

        yield return new WaitForSeconds(1f);

        battleState.SetEnemyDamageAnimationPlaying(false);
        enemyHealthBar?.Hide();
    }

    private class LLMResponse
    {
        public string emotion;
        public string response;
    }

    public bool CheckLLMResponse(MonoBehaviour monoBehaviour, string content)
    {
        Debug.Log(content);
        try
        {
            LLMResponse res = JsonUtility.FromJson<LLMResponse>(content);
            if (!enemyObject.AvailableEmotions.Contains(res.emotion))
            {
                Debug.LogError("invalid emotion: " + res.emotion);
                return false;
            }
            enemyEmotion.Value = res.emotion;
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError("Exception while parsing LLM Response: " + e);
            return false;
        }
    }

    public void HandleLLMResponse(MonoBehaviour monoBehaviour, string content)
    {
        LLMResponse res = JsonUtility.FromJson<LLMResponse>(content);
        enemyEmotion.Value = res.emotion;
        OnDisplayEnemyDialogue(monoBehaviour, res.response);
    }

    public void OnDisplayEnemyDialogue(MonoBehaviour monoBehaviour, string content)
    {
        enemyDialogueHandler.DisplayText(content);
    }
}
