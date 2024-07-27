using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BattleWinTextRunner : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueUITypewriterEffect typewriterEffect;
    [SerializeField] private GameEventObject onReturnToOverworld;
    private string[] textToDisplay;
    //void Start()
    //{
        //textToDisplay = new string[2];
        //textToDisplay[0] = "Battle won!";
        //textToDisplay[1] = "You gained " + enemyLoadedTrackerObject.LoadedEnemy.ExpReward + " EXP and " + enemyLoadedTrackerObject.LoadedEnemy.MoneyReward + " money.";
        //StartCoroutine(StepThroughWinDialogue());
    //}

    public void Init(string[] textToDisplay)
    {
        this.textToDisplay = textToDisplay;
    }

    public void BeginBattleWinSequence()
    {
        StepThroughWinDialogue();
    }

    private IEnumerator StepThroughWinDialogue()
    {
        for (int i = 0; i < textToDisplay.Length; i++)
        {
            string dialogue = textToDisplay[i];
            typewriterEffect.Run(dialogue, textLabel);
            while (typewriterEffect.IsRunning)
            {
                yield return null;
            }
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        // Now go back to the overworld
        onReturnToOverworld.Raise();
    }
}
