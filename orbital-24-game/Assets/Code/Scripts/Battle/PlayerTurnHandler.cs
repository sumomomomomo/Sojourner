using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenAI;
using PlasticPipe.PlasticProtocol.Messages;
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
    [SerializeField] private IntReference llmShot;

    private OpenAIApi openai;

    private void Start()
    {
        SetOpenAI(new OpenAIApi());
        playerTalkInputField.enabled = true;
        playerTalkInputField.gameObject.SetActive(false);
        playerTalkInputHandlerObject.SetActive(false);
    }

    public void SetOpenAI(OpenAIApi openAIApi)
    {
        openai = openAIApi;
    }
    
    public void OnPlayerTurnStart()
    {
        currentStrategy.ToDefaultValidStrategy();
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
            await HandleTalkOnTurnEnd();
        }
    }

    private async Task HandleTalkOnTurnEnd()
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

        CreateChatCompletionRequest req = new()
        {
            Model = "gpt-4o-mini",
            Messages = chatMessageObjectsTracker.GetChatMessages(),
            ResponseFormat = new ResponseFormat
            {
                Type = ResponseType.JsonObject
            },
            Temperature = 0.6f
        };

        int triesLeft = llmShot.Value;
        bool isSuccessful = false;
        ChatMessage message;
        while (triesLeft > 0)
        {
            CreateChatCompletionResponse completionResponse = await openai.CreateChatCompletion(req);
            triesLeft--;
            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();

                bool isOutputValid = enemyHandler.CheckLLMResponse(message.Content);
                if (isOutputValid)
                {
                    chatMessageObjectsTracker.AddChatMessage(message);
                    isSuccessful = true;
                    break;
                }
                else
                {
                    Debug.LogWarning("This prompt generated invalid output.");
                    continue;
                }
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
                continue;
            }
        }
        
        if (!isSuccessful)
        {
            Debug.LogWarning("Reached max tries but was not successful. Removing last message.");
            chatMessageObjectsTracker.RemoveLastMessage();
        }
        else
        {
            enemyHandler.HandleLLMResponse(message.Content);
        }

        battleState.SetPlayerTalking(false);
        playerTalkInputField.gameObject.SetActive(false);
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
