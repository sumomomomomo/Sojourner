using System.Collections;
using System.Collections.Generic;
using OpenAI;
using TMPro;
using UnityEngine;

public class PlayerTurnHandler : MonoBehaviour
{
    [SerializeField] private BattleStrategyTrackerObject currentStrategy;
    [SerializeField] private BoundTargetInstructionsObject boundTargetInstructionsObject;
    [SerializeField] private PlayerBoundsTarget playerBoundsTarget;
    [SerializeField] private PlayerBoundsTarget playerBoundsTargetTalk;
    [SerializeField] private BattleState battleState;
    [SerializeField] private GameEventObject onUpdateBounds;
    [SerializeField] private ChatMessageObjectsTracker chatMessageObjectsTracker;

    [SerializeField] private TMP_InputField playerTalkInputField;
    [SerializeField] private GameObject playerTalkInputHandlerObject;
    [SerializeField] private EnemyHandler enemyHandler;

    [SerializeField] private FloatReference llmTemperature;

    private OpenAIApi openai = new();

    private void Start()
    {
        playerTalkInputField.enabled = true;
        playerTalkInputField.gameObject.SetActive(false);
        playerTalkInputHandlerObject.SetActive(false);
    }
    public void OnPlayerTurnStart()
    {
        boundTargetInstructionsObject.PlayerBoundsTarget = playerBoundsTarget;
        onUpdateBounds.Raise();
    }

    public async void OnPlayerTurnEnd()
    {
        if (!battleState.IsPlayerTalking())
        {
            currentStrategy.OnExecuteStrategy().Invoke();
        }
        else
        {
            // Something should have been typed -- handle it here

            //battleState.SetPlayerTalking(false);

            Debug.Log(playerTalkInputField.text);
            playerTalkInputField.enabled = false;
            playerTalkInputHandlerObject.SetActive(false);


            // Do stuff here

            chatMessageObjectsTracker.AddChatMessage(
                new ChatMessageObject("user", playerTalkInputField.text)
                );
            
            CreateChatCompletionResponse completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-4o-mini",
                Messages = chatMessageObjectsTracker.GetChatMessages(),
                ResponseFormat = new ResponseFormat
                {
                    Type = ResponseType.JsonObject
                },
                Temperature = 0.6f
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                ChatMessage message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();

                // test to see if output is valid
                bool isOutputValid = enemyHandler.OnLLMResponse(message.Content);
                if (isOutputValid)
                {
                    chatMessageObjectsTracker.AddChatMessage(message);
                }
                else
                {
                    Debug.LogWarning("This prompt generated invalid output. No message added.");
                    chatMessageObjectsTracker.RemoveLastMessage();
                    // TODO: rerun?
                }
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

            battleState.SetPlayerTalking(false);
            playerTalkInputField.gameObject.SetActive(false);
        }
    }

    public void OnPlayerTalkStart()
    {
        battleState.SetPlayerTalking(true);
        // handle all the event calls to setup
        boundTargetInstructionsObject.PlayerBoundsTarget = playerBoundsTargetTalk;
        onUpdateBounds.Raise();

        StartCoroutine(MakePlayerTalkFieldVisible());
    }

    private IEnumerator MakePlayerTalkFieldVisible()
    {
        while (!battleState.IsPlayerTalkInputEnabled())
        {
            yield return null;
        }
        playerTalkInputField.enabled = true;
        playerTalkInputField.gameObject.SetActive(true);
        playerTalkInputField.text = "";
        playerTalkInputHandlerObject.SetActive(true);
    }
}
