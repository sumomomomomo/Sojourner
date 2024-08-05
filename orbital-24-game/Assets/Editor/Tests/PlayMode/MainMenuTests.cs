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

public class MainMenuTests : InputTestFixture
{
    private GameEventObject onNewGame;
    private BoolVariable isCutscenePlayingOverworld;
    //private InputTestFixture input = new InputTestFixture();

    [OneTimeSetUp]
    public void GetVars()
    {
        onNewGame = Resources.Load<GameEventObject>("ScriptableObjects/Events/MainMenu/OnNewGame");
        isCutscenePlayingOverworld = Resources.Load<BoolVariable>("ScriptableObjects/Overworld/IsCutscenePlayingOverworld");
    }

    public override void Setup()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    [UnityTest]
    public IEnumerator NewGameButtonBringsToOverworldSceneAndStartsCutscene()
    {
        onNewGame.Raise();
        yield return new WaitForSeconds(3f);
        Assert.AreEqual(isCutscenePlayingOverworld.Value, true);
    }
}
