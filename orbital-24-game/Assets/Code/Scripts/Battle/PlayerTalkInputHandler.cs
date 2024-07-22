using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerTalkInputHandler : MonoBehaviour
{
    [SerializeField] private BattleState battleState;
    [SerializeField] private TMP_InputField playerTalkInputField;
    [SerializeField] private GameEventObject onForceChangeTurn;

    private void Update()
    {
        if (battleState.IsPlayerTalkInputEnabled() && playerTalkInputField.isFocused == false)
        {
            playerTalkInputField.ActivateInputField();
            playerTalkInputField.Select();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            onForceChangeTurn.Raise();
        }
    }

}
