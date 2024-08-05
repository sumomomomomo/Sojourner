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

public class GoblinGuardTests : InputTestFixture
{
    private BattleState battleState;
    private EnemyLoadedTrackerObject enemyLoadedTrackerObject;
    private EnemyObject goblinGuardEnemyObject;

    private FloatVariable playerAgi;
    private IntVariable playerAtk;
    private IntVariable playerDef;
    private IntVariable playerHP;
    private IntVariable playerBaseMaxHP;

    private BattleStrategyTrackerObject battleStrategyTrackerObject;

    [OneTimeSetUp]
    public void GetVars()
    {
        battleState = Resources.Load<BattleState>("ScriptableObjects/Battle/BattleState");
        enemyLoadedTrackerObject = Resources.Load<EnemyLoadedTrackerObject>("ScriptableObjects/Battle/EnemyLoadedTrackerObject");
        goblinGuardEnemyObject = Resources.Load<EnemyObject>("Prefabs/Battle/GoblinGuard/GoblinGuardObject");

        playerAgi = Resources.Load<FloatVariable>("ScriptableObjects/Player/PlayerAgility");
        playerAtk = Resources.Load<IntVariable>("ScriptableObjects/Player/PlayerBaseAtk");
        playerDef = Resources.Load<IntVariable>("ScriptableObjects/Player/PlayerBaseDef");
        playerHP = Resources.Load<IntVariable>("ScriptableObjects/Player/PlayerHP");
        playerBaseMaxHP = Resources.Load<IntVariable>("ScriptableObjects/Player/PlayerBaseMaxHP");

        battleStrategyTrackerObject = Resources.Load<BattleStrategyTrackerObject>("ScriptableObjects/Battle/CurrentStrategy");

        enemyLoadedTrackerObject.LoadEnemy(goblinGuardEnemyObject);
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
    public IEnumerator PlayerTurnOnSceneLoad()
    {
        yield return new WaitForSeconds(1f);
        Assert.AreEqual(true, battleState.IsPlayerTurn());
    }

    [UnityTest]
    public IEnumerator FreezeTurnDisablesTurnHandler()
    {
        battleState.SetFreezeTurn(true);    
        yield return null;
        Assert.AreEqual(battleState.IsTurnHandlerActive(), false);
        battleState.SetFreezeTurn(false);
    }

    [UnityTest]
    public IEnumerator PlayerOneshotsGoblinAndOnBattleWinIsCalled()
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
    public IEnumerator DefendHeals4HP()
    {
        TurnHandler turnHandler = GameObject.FindObjectOfType<TurnHandler>();
        playerHP.Value = 10;

        // force selection of def
        battleStrategyTrackerObject.ToPreviousStrategy();
        battleStrategyTrackerObject.ToPreviousStrategy();
        battleStrategyTrackerObject.ToPreviousStrategy();
        battleStrategyTrackerObject.ToPreviousStrategy();
        battleStrategyTrackerObject.ToNextStrategy();

        turnHandler.ChangeTurn();

        yield return new WaitForSeconds(1f);

        Assert.AreEqual(14, playerHP.Value);
    }

    [UnityTest]
    public IEnumerator PlayerDyingSetsBattleLoseToTrue()
    {
        TurnHandler turnHandler = GameObject.FindObjectOfType<TurnHandler>();
        PlayerDamageTaker playerDamageTaker = GameObject.FindObjectOfType<PlayerDamageTaker>();

        // force selection of def
        battleStrategyTrackerObject.ToPreviousStrategy();
        battleStrategyTrackerObject.ToPreviousStrategy();
        battleStrategyTrackerObject.ToPreviousStrategy();
        battleStrategyTrackerObject.ToPreviousStrategy();
        battleStrategyTrackerObject.ToNextStrategy();

        turnHandler.ChangeTurn();

        yield return new WaitForSeconds(3.5f);

        playerDamageTaker.takeDamage(1000);

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(true, battleState.IsBattleLose());
    }

    [UnityTest]
    public IEnumerator PlayerWhenDefendingTakes1Dmg()
    {
        TurnHandler turnHandler = GameObject.FindObjectOfType<TurnHandler>();
        PlayerDamageTaker playerDamageTaker = GameObject.FindObjectOfType<PlayerDamageTaker>();
        playerHP.Value = 20;

        // force selection of def
        battleStrategyTrackerObject.ToPreviousStrategy();
        battleStrategyTrackerObject.ToPreviousStrategy();
        battleStrategyTrackerObject.ToPreviousStrategy();
        battleStrategyTrackerObject.ToPreviousStrategy();
        battleStrategyTrackerObject.ToNextStrategy();

        turnHandler.ChangeTurn();

        playerDamageTaker.takeDamage(1);

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(19, playerHP.Value);
    }

    [UnityTest]
    public IEnumerator TestTalkWhereGoblinBecomesScaredAndSpareHim()
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
                        Content = "{\"response\":\"Scram, human!\",\"emotion\":\"terrified\"}"
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

        yield return new WaitForSeconds(10f);

        // force selection of spare
        battleStrategyTrackerObject.ToNextStrategy();
        battleStrategyTrackerObject.ToNextStrategy();
        battleStrategyTrackerObject.ToNextStrategy();
        battleStrategyTrackerObject.ToNextStrategy();

        turnHandler.ChangeTurn();

        yield return new WaitForSeconds(1f);
        Assert.AreEqual(true, battleState.IsBattleWin());

    }
}
