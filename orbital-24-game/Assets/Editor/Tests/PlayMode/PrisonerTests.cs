using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using OpenAI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class PrisonerTests : InputTestFixture
{
    private BattleState battleState;
    private EnemyLoadedTrackerObject enemyLoadedTrackerObject;
    private EnemyObject prisonerEnemyObject;

    private FloatVariable playerAgi;
    private IntVariable playerAtk;
    private IntVariable playerDef;
    private IntVariable playerHP;
    private IntVariable playerBaseMaxHP;

    private StringVariable enemyEmotion;

    private BattleStrategyTrackerObject battleStrategyTrackerObject;
    private BattleStrategyObject attack;
    private GameEventObject onBattleWin;

    private InputTestFixture input = new InputTestFixture();

    [OneTimeSetUp]
    public void GetVars()
    {
        battleState = Resources.Load<BattleState>("ScriptableObjects/Battle/BattleState");
        enemyLoadedTrackerObject = Resources.Load<EnemyLoadedTrackerObject>("ScriptableObjects/Battle/EnemyLoadedTrackerObject");
        prisonerEnemyObject = Resources.Load<EnemyObject>("Prefabs/Battle/Prisoner/PrisonerObject");
        enemyEmotion = Resources.Load<StringVariable>("ScriptableObjects/Battle/EnemyEmotion");

        playerAgi = Resources.Load<FloatVariable>("ScriptableObjects/Player/PlayerAgility");
        playerAtk = Resources.Load<IntVariable>("ScriptableObjects/Player/PlayerBaseAtk");
        playerDef = Resources.Load<IntVariable>("ScriptableObjects/Player/PlayerBaseDef");
        playerHP = Resources.Load<IntVariable>("ScriptableObjects/Player/PlayerHP");
        playerBaseMaxHP = Resources.Load<IntVariable>("ScriptableObjects/Player/PlayerBaseMaxHP");

        attack = Resources.Load<BattleStrategyObject>("ScriptableObjects/Battle/BattleStrategyObjects/Attack");
        onBattleWin = Resources.Load<GameEventObject>("ScriptableObjects/Battle/GameEventObjects/OnBattleWin");
        battleStrategyTrackerObject = Resources.Load<BattleStrategyTrackerObject>("ScriptableObjects/Battle/CurrentStrategy");

        enemyLoadedTrackerObject.LoadEnemy(prisonerEnemyObject);
    }

    public override void Setup()
    {
        playerAgi.Value = 5; // TODO find another way to do this that doesn't modify scriptableobject?
        playerAtk.Value = 7;
        playerDef.Value = 4;
        playerHP.Value = 20;
        playerBaseMaxHP.Value = 20;
        SceneManager.LoadScene("BattleScene");
    }

    [UnityTest]
    public IEnumerator PlayerOneshotsPrisonerAndOnBattleWinIsCalled()
    {
        TurnHandler turnHandler = GameObject.FindObjectOfType<TurnHandler>();

        playerAtk.Value = 99999;

        // force selection of attack
        battleStrategyTrackerObject.ToPreviousStrategy();
        battleStrategyTrackerObject.ToPreviousStrategy();
        battleStrategyTrackerObject.ToPreviousStrategy();
        battleStrategyTrackerObject.ToPreviousStrategy();

        turnHandler.ChangeTurn();

        yield return new WaitForSeconds(5f);

        Assert.AreEqual(true, battleState.IsBattleWin());
    }

    [UnityTest]
    public IEnumerator BeatPrisonerHalfDeadAndSeeHimGetSerious()
    {
        TurnHandler turnHandler = GameObject.FindObjectOfType<TurnHandler>();

        playerAtk.Value = 37;

        // force selection of attack
        battleStrategyTrackerObject.ToPreviousStrategy();
        battleStrategyTrackerObject.ToPreviousStrategy();
        battleStrategyTrackerObject.ToPreviousStrategy();
        battleStrategyTrackerObject.ToPreviousStrategy();

        turnHandler.ChangeTurn();

        yield return new WaitForSeconds(5f);

        Assert.AreEqual("angry", enemyEmotion.Value);
    }

    [UnityTest]
    public IEnumerator TestTalkWherePrisonerBecomesDocileAndSpareHim()
    {
        TurnHandler turnHandler = GameObject.FindObjectOfType<TurnHandler>();
        PlayerTurnHandler playerTurnHandler = GameObject.FindObjectOfType<PlayerTurnHandler>();

        CreateChatCompletionResponse mockChatCompletionResponse = new()
        {
            Choices = new List<ChatChoice>
            {
                new ChatChoice()
                {
                    Message = new ChatMessage()
                    {
                        Role = "assistant",
                        Content = "{\"response\":\"I am convinced.\",\"convincedLevel\":\"5\"}"
                    }
                }
            }
        };

        // mock openaiapi
        var mockOpenAIApi = Substitute.For<OpenAIApi>("", "");
        mockOpenAIApi.CreateChatCompletion(default).ReturnsForAnyArgs(mockChatCompletionResponse);
        playerTurnHandler.SetOpenAI(mockOpenAIApi);

        // force selection of talk
        battleStrategyTrackerObject.ToNextStrategy();
        battleStrategyTrackerObject.ToNextStrategy();
        battleStrategyTrackerObject.ToNextStrategy();
        battleStrategyTrackerObject.ToNextStrategy();

        // mock keyboard
        var keyboard = InputSystem.AddDevice<Keyboard>();

        turnHandler.ChangeTurn();

        yield return new WaitForSeconds(1f);

        turnHandler.ChangeTurn();

        yield return new WaitForSeconds(3f);
        
        Press(keyboard.spaceKey);
        yield return null;
        Release(keyboard.spaceKey);
        yield return null;
        Press(keyboard.spaceKey);
        yield return null;
        Release(keyboard.spaceKey);
        yield return null;

        yield return new WaitForSeconds(7f);

        // force selection of spare
        battleStrategyTrackerObject.ToNextStrategy();
        battleStrategyTrackerObject.ToNextStrategy();
        battleStrategyTrackerObject.ToNextStrategy();
        battleStrategyTrackerObject.ToNextStrategy();

        turnHandler.ChangeTurn();

        yield return new WaitForSeconds(1f);
        Assert.AreEqual(battleState.IsBattleWin(), true);

    }
}
