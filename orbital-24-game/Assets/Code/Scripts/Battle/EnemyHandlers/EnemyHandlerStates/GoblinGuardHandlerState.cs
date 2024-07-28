using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Enemy Handler States/Goblin Guard")]
public class GoblinGuardHandlerState : ScriptableObject, IEnemyHandlerState
{
    [SerializeField] private EnemyObject enemyObject;
    [SerializeField] private BoundTargetInstructionsObject boundTargetInstructionsObject;
    [SerializeField] private BoolVariable isFreezeTurn;
    [SerializeField] private StringVariable enemyEmotion;
    [SerializeField] private BattleStrategyObject run;
    [SerializeField] private BattleStrategyObject talk;
    [SerializeField] private IntVariable enemyAtk;
    [SerializeField] private IntVariable enemyDef;
    [SerializeField] private FloatVariable enemyAgi;
    private TurnHandler turnHandler;
    private List<GameObject> instantiatedObjects = new();
    private GameObject spriteObject;
    private GameObject healthBarSliderCanvasObject;
    private GameObject dialogueBoxObject;
    private EnemySpriteHandler enemySpriteHandler;
    private EnemyHealthBar enemyHealthBar;
    private EnemyDialogueHandler enemyDialogueHandler;
    public void OnBattleStart(MonoBehaviour monoBehaviour, TurnHandler turnHandler, GameObject _)
    {
        //Init turnhandler
        this.turnHandler = turnHandler;

        // Disable Run
        run.Disable();

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
        enemyEmotion.Value = "angry";

        ((IEnemyHandlerState) this).OnDisplayEnemyDialogueExpire(monoBehaviour, "Go back! Cell!", enemyDialogueHandler, 2);
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
        if (enemyEmotion.Value != "terrified")
        {
            AttackPattern chosenAttack = enemyObject.AttackPatterns[UnityEngine.Random.Range(0, 2)];
            boundTargetInstructionsObject.SetAndRaiseUpdateBounds(chosenAttack.PlayerBoundsTarget);
            monoBehaviour.StartCoroutine(DoAttack(chosenAttack));
        }
        else
        {
            // do nothing
            boundTargetInstructionsObject.SetAndRaiseUpdateBounds(enemyObject.AttackPatterns[0].PlayerBoundsTarget);
            monoBehaviour.StartCoroutine(DoNothing(monoBehaviour));
        }

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

    private IEnumerator DoNothing(MonoBehaviour monoBehaviour)
    {
        yield return new WaitForSeconds(0.25f);
        while (isFreezeTurn.Value)
        {
            yield return new WaitForSeconds(0.01f);
        }
        turnHandler.SetTimeLeftValues(4);
        ((IEnemyHandlerState) this).OnDisplayEnemyDialogueExpire(monoBehaviour, "*Goblin Guard is trembling in fear*", enemyDialogueHandler, 2);
        yield return new WaitForSeconds(3.25f);
        
    }

    public void OnPlayerWin(MonoBehaviour monoBehaviour)
    {
        // sprite fades away
        Destroy(spriteObject);
        Destroy(healthBarSliderCanvasObject);
        Destroy(dialogueBoxObject);

        // Do the behaviours
        enemyObject.OnBattleWin();
        OnEnemyTurnEnd(monoBehaviour);

        // wintext appears
        GameObject winText = Instantiate(enemyObject.WinTextPrefab);
        BattleWinTextRunner battleWinTextRunner = winText.GetComponentInChildren<BattleWinTextRunner>();
        battleWinTextRunner.Init(new string[] {
            "You won.",
            "You gained nothing."
        });
        battleWinTextRunner.BeginBattleWinSequence();
    }

    public void OnPlayerRun(MonoBehaviour monoBehaviour, BattleState battleState)
    {
        // in this case, instant win
        battleState.SetBattleWin(true);

        enemyObject.OnSpare();

        Destroy(healthBarSliderCanvasObject);
        Destroy(dialogueBoxObject);

        GameObject winText = Instantiate(enemyObject.WinTextPrefab);
        BattleWinTextRunner battleWinTextRunner = winText.GetComponentInChildren<BattleWinTextRunner>();
        battleWinTextRunner.Init(new string[] {
            "You let him run away.",
        });
        battleWinTextRunner.BeginBattleWinSequence();
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
        battleState.SetEnemyDamageAnimationPlaying(false);
        yield return new WaitForSeconds(1f);

        if (enemyHealthBar != null && afterHP > 0)
            enemyHealthBar.Hide();
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
        OnChangeEmotion(res.emotion);
        ((IEnemyHandlerState) this).OnDisplayEnemyDialogue(monoBehaviour, res.response, enemyDialogueHandler);
    }

    private void OnChangeEmotion(string newEmotion)
    {
        enemyEmotion.Value = newEmotion;
        switch (newEmotion)
        {
            case "angry":
                enemyAtk.Value = enemyObject.Atk;
                enemyDef.Value = enemyObject.Def;
                enemyAgi.Value = enemyObject.Agility;
                break;
            case "scared":
                enemyAtk.Value = enemyObject.Atk;
                enemyDef.Value = enemyObject.Def - 2;
                enemyAgi.Value = enemyObject.Agility - 2;
                break;
            case "terrified":
                talk.Disable();
                run.Enable();
                run.SetText("Spare");
                run.SetColor(Color.yellow);
                enemyAtk.Value = enemyObject.Atk;
                enemyDef.Value = enemyObject.Def - 2;
                enemyAgi.Value = -999;
                break;
        }
    }
}
