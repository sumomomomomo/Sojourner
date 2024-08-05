using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class OverworldTests : InputTestFixture
{
    private BoolVariable isOverworldMenuOpen;
    private BoolVariable isDialogueBoxOpen;
    private BoolVariable isCutscenePlayingOverworld;
    private DialogueObject testDialogue;

    [OneTimeSetUp]
    public void GetVars()
    {
        isOverworldMenuOpen = Resources.Load<BoolVariable>("ScriptableObjects/Overworld/IsOverworldMenuOpen");
        isDialogueBoxOpen = Resources.Load<BoolVariable>("ScriptableObjects/Overworld/IsDialogueBoxOpen");
        isCutscenePlayingOverworld = Resources.Load<BoolVariable>("ScriptableObjects/Overworld/IsCutscenePlayingOverworld");

        testDialogue = Resources.Load<DialogueObject>("ScriptableObjects/Dialogue/Debug/2_PlayerNoticesDoor");
    }

    public override void Setup()
    {
        isOverworldMenuOpen.Value = false;
        isCutscenePlayingOverworld.Value = false;
        SceneManager.LoadScene("DebugJailScene");
    }

    [UnityTest]
    public IEnumerator InventoryOpensWhenPressI()
    {
        // mock keyboard
        var keyboard = InputSystem.AddDevice<Keyboard>();

        yield return new WaitForSeconds(0.1f);

        Press(keyboard.iKey);
        yield return null;
        Release(keyboard.iKey);
        yield return null;

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(true, isOverworldMenuOpen.Value);
    }

    [UnityTest]
    public IEnumerator DialogueBoxOpensOnInitiateDialogue()
    {
        var player = GameObject.FindObjectOfType<Player>();

        player.DialogueUI.ShowDialogue(testDialogue);

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(true, isDialogueBoxOpen.Value);
    }
}
