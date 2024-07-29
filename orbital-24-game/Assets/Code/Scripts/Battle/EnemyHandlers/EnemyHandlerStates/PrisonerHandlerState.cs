using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Enemy Handler States/Prisoner")]
public class PrisonerHandlerState : ScriptableObject, IEnemyHandlerState
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
    private List<GameObject> instantiatedObjects = new();
    private TurnHandler turnHandler;
    private GameObject spriteObject;
    private GameObject healthBarSliderCanvasObject;
    private GameObject dialogueBoxObject;
    private EnemySpriteHandler enemySpriteHandler;
    private EnemyHealthBar enemyHealthBar;
    private EnemyDialogueHandler enemyDialogueHandler;

    private TripleCombo tripleCombo;
    private RectangleCombo rectangleCombo;
    private BlueOrangeWaves blueOrangeWaves;

    private int lastIndex;
    private List<int> attackIndices;

    private int convincedLevel;
    private bool hardMode;
    public void OnBattleStart(MonoBehaviour monoBehaviour, TurnHandler turnHandler, GameObject player)
    {
        //Init convincedLevel
        convincedLevel = 0;
        hardMode = false;

        //Init attack choosing system
        attackIndices = new() {0, 1, 2};
        ListShuffle.Shuffle(attackIndices);
        lastIndex = 0;

        //Init turnhandler
        this.turnHandler = turnHandler;

        // Disable Run
        run.Disable();

        // Get specific scripted attacks
        // Note: Index specific
        instantiatedObjects = new()
        {
            Instantiate(enemyObject.AttackPatterns[0].AttackPrefab),
            Instantiate(enemyObject.AttackPatterns[1].AttackPrefab),
            Instantiate(enemyObject.AttackPatterns[2].AttackPrefab)
        };
        tripleCombo = instantiatedObjects[0].GetComponentInChildren<TripleCombo>();
        tripleCombo.OnBattleStart(player.gameObject);
        rectangleCombo = instantiatedObjects[1].GetComponentInChildren<RectangleCombo>();
        rectangleCombo.OnBattleStart(player.gameObject);
        blueOrangeWaves = instantiatedObjects[2].GetComponentInChildren<BlueOrangeWaves>();
        blueOrangeWaves.OnBattleStart(player.gameObject);

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
        enemyEmotion.Value = "neutral";

        ((IEnemyHandlerState) this).OnDisplayEnemyDialogueExpire(monoBehaviour, "Sorry king, freedom is freedom.", enemyDialogueHandler, 2);
    }
    public void OnEnemyTurnEnd(MonoBehaviour monoBehaviour)
    {
        tripleCombo.OnEnemyAttackEnd();
        rectangleCombo.OnEnemyAttackEnd();
        blueOrangeWaves.OnEnemyAttackEnd();
    }

    private int GetNextAttackIndex()
    {
        lastIndex++;
        if (lastIndex == attackIndices.Count)
        {
            lastIndex = 0;
            ListShuffle.Shuffle(attackIndices);
        }
        return attackIndices[lastIndex];
    }

    public void OnEnemyTurnStart(MonoBehaviour monoBehaviour)
    {
        if (enemyEmotion.Value != "convinced")
        {
            switch (GetNextAttackIndex())
            {
                case 0:
                    if (hardMode)
                    {
                        boundTargetInstructionsObject.SetAndRaiseUpdateBounds(enemyObject.AttackPatterns[0].PlayerBoundsTarget);
                        tripleCombo.SetVariant(1);
                        monoBehaviour.StartCoroutine(DoAttack(tripleCombo));
                    }
                    else
                    {
                        boundTargetInstructionsObject.SetAndRaiseUpdateBounds(enemyObject.AttackPatterns[0].PlayerBoundsTarget);
                        monoBehaviour.StartCoroutine(DoAttack(tripleCombo));
                    }
                    break;
                case 1:
                    boundTargetInstructionsObject.SetAndRaiseUpdateBounds(enemyObject.AttackPatterns[1].PlayerBoundsTarget);
                    if (hardMode)
                    {
                        monoBehaviour.StartCoroutine(DoAttack(blueOrangeWaves));
                        monoBehaviour.StartCoroutine(DoAttack(rectangleCombo));
                    }
                    else
                    {
                        monoBehaviour.StartCoroutine(DoAttack(rectangleCombo));
                    }
                    break;
                case 2:
                    boundTargetInstructionsObject.SetAndRaiseUpdateBounds(enemyObject.AttackPatterns[2].PlayerBoundsTarget);
                    if (hardMode)
                    {
                        monoBehaviour.StartCoroutine(DoAttack(blueOrangeWaves));
                        monoBehaviour.StartCoroutine(DoAttack(rectangleCombo));
                    }
                    else
                    {
                        monoBehaviour.StartCoroutine(DoAttack(blueOrangeWaves));
                    }
                    break;
            }
        }
        else
        {
            // do nothing
            boundTargetInstructionsObject.SetAndRaiseUpdateBounds(enemyObject.AttackPatterns[0].PlayerBoundsTarget);
            monoBehaviour.StartCoroutine(DoNothing(monoBehaviour));
        }
    }
    private IEnumerator DoAttack(IScriptedAttack scriptedAttack)
    {
        // TODO
        yield return new WaitForSeconds(0.25f);
        while (isFreezeTurn.Value)
        {
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.25f);
        scriptedAttack.OnEnemyAttackStart();
        // instantiatedObjects.Add(Instantiate(attackPattern.AttackPrefab));
    }

    private IEnumerator DoNothing(MonoBehaviour monoBehaviour)
    {
        yield return new WaitForSeconds(0.25f);
        while (isFreezeTurn.Value)
        {
            yield return new WaitForSeconds(0.01f);
        }
        turnHandler.SetTimeLeftValues(4);
        ((IEnemyHandlerState) this).OnDisplayEnemyDialogueExpire(monoBehaviour, "*Prisoner is deep in thought*", enemyDialogueHandler, 2);
        yield return new WaitForSeconds(3.25f);
        
    }

    public void OnPlayerWin(MonoBehaviour monoBehaviour)
    {
        // sprite fades away
        Destroy(spriteObject);
        Destroy(healthBarSliderCanvasObject);
        Destroy(dialogueBoxObject);
        for (int i = 0; i < instantiatedObjects.Count; i++)
        {
            Destroy(instantiatedObjects[i]);
        }

        // Do the behaviours
        enemyObject.OnBattleWin();

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
        for (int i = 0; i < instantiatedObjects.Count; i++)
        {
            Destroy(instantiatedObjects[i]);
        }
        // in this case, instant win
        battleState.SetBattleWin(true);

        enemyObject.OnSpare();

        Destroy(healthBarSliderCanvasObject);
        Destroy(dialogueBoxObject);

        GameObject winText = Instantiate(enemyObject.WinTextPrefab);
        BattleWinTextRunner battleWinTextRunner = winText.GetComponentInChildren<BattleWinTextRunner>();
        battleWinTextRunner.Init(new string[] {
            "You managed to convince him to stand down.",
        });
        battleWinTextRunner.BeginBattleWinSequence();
    }

    public void OnTakeDamage(MonoBehaviour monoBehaviour, IntVariable enemyHP, BattleState battleState)
    {
        battleState.SetEnemyDamageAnimationPlaying(true);
        monoBehaviour.StartCoroutine(AnimateDamageTaken(enemyHP, battleState, monoBehaviour));
    }

    private IEnumerator AnimateDamageTaken(IntVariable enemyHP, BattleState battleState, MonoBehaviour monoBehaviour)
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

        if (afterHP > 0)
        {
            if (enemyHealthBar != null)
                enemyHealthBar.Hide();

            if (!hardMode && afterHP > 0 && afterHP < enemyObject.MaxHP * 0.67 || convincedLevel >= 2)
            {
                hardMode = true;
                OnChangeConvincedLevel(-1);
                ((IEnemyHandlerState) this).OnDisplayEnemyDialogue(monoBehaviour, "I'm not going to hold back any more.", enemyDialogueHandler);
            }
        }
        
        battleState.SetEnemyDamageAnimationPlaying(false);
    }

    private class LLMResponse
    {
        public int convincedLevel;
        public string response;
    }

    public bool CheckLLMResponse(MonoBehaviour monoBehaviour, string content)
    {
        Debug.Log(content);
        try
        {
            LLMResponse res = JsonUtility.FromJson<LLMResponse>(content);
            int testConvincedLevel = res.convincedLevel;
            if (testConvincedLevel < 0 || testConvincedLevel > 5)
            {
                Debug.LogError("invalid convinced level: " + res.convincedLevel);
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
        OnChangeConvincedLevel(res.convincedLevel);
        ((IEnemyHandlerState) this).OnDisplayEnemyDialogue(monoBehaviour, res.response, enemyDialogueHandler);
    }

    private void OnChangeConvincedLevel(int newConvincedLevel)
    {
        // TODO
        convincedLevel = newConvincedLevel;
        if (convincedLevel <= 0)
        {
            enemyEmotion.Value = "angry";
            talk.Disable();
            enemyAtk.Value = enemyObject.Atk + 4;
            enemyDef.Value = enemyObject.Def - 4;
            enemyAgi.Value = enemyObject.Agility + 5;
        }
        else if (convincedLevel >= 2 && convincedLevel < 4)
        {
            enemyEmotion.Value = "convinced";
            enemyAtk.Value = enemyObject.Atk;
            enemyDef.Value = -20;
            enemyAgi.Value = enemyObject.Agility;
        }
        else if (convincedLevel >= 4)
        {
            enemyEmotion.Value = "convinced";
            enemyAtk.Value = enemyObject.Atk;
            enemyDef.Value = -99;
            enemyAgi.Value = enemyObject.Agility;
            talk.Disable();
            run.Enable();
            run.SetText("Spare");
            run.SetColor(Color.yellow);
        }

    }
}
