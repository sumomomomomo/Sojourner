using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BattleWinTextRunner : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private DialogueUITypewriterEffect typewriterEffect;
    [SerializeField] private EnemyLoadedTrackerObject enemyLoadedTrackerObject;
    [SerializeField] private GameEventObject onReturnToOverworld;
    private string[] textToDisplay;
    void Start()
    {
        textToDisplay = new string[2];
        textToDisplay[0] = "Battle won!";
        textToDisplay[1] = "You gained " + enemyLoadedTrackerObject.LoadedEnemy.ExpReward + " EXP and " + enemyLoadedTrackerObject.LoadedEnemy.MoneyReward + " money.";
        StartCoroutine(StepThroughWinDialogue());
    }

    private IEnumerator StepThroughWinDialogue()
    {
        for (int i = 0; i < textToDisplay.Length; i++)
        {
            string dialogue = textToDisplay[i];
            yield return typewriterEffect.Run(dialogue, textLabel);
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }

        // Now go back to the overworld
        onReturnToOverworld.Raise();
    }
}
