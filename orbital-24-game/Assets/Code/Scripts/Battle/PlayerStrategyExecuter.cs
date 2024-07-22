using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStrategyExecuter : MonoBehaviour
{
    [SerializeField] private BattleStrategyTrackerObject currentStrategy;
    [SerializeField] private BoundTargetInstructionsObject boundTargetInstructionsObject;
    [SerializeField] private PlayerBoundsTarget playerBoundsTarget;
    [SerializeField] private PlayerBoundsTarget playerBoundsTargetTalk;
    [SerializeField] private BattleState battleState;
    [SerializeField] private GameEventObject onUpdateBounds;

    [SerializeField] private TMP_InputField playerTalkInputField;
    [SerializeField] private GameObject playerTalkInputHandlerObject;

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

    public void OnPlayerTurnEnd()
    {
        if (!battleState.IsPlayerTalking())
        {
            currentStrategy.OnExecuteStrategy().Invoke();
        }
        else
        {
            // Something should have been typed -- handle it here

            battleState.SetPlayerTalking(false);

            Debug.Log(playerTalkInputField.text);

            playerTalkInputField.gameObject.SetActive(false);
            playerTalkInputHandlerObject.SetActive(false);
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
        playerTalkInputField.gameObject.SetActive(true);
        playerTalkInputField.text = "";
        playerTalkInputHandlerObject.SetActive(true);
    }
}
