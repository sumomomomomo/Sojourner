using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyDialogueHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text enemyDialogueTextField;
    [SerializeField] private GameObject backing;
    [SerializeField] private DialogueUITypewriterEffect dialogueUITypewriterEffect;
    [SerializeField] private BoolVariable isEnemySpeaking;
    public void DisplayText(string text)
    {
        Show();
        isEnemySpeaking.Value = true;
        StartCoroutine(DisplayTextSingle(text));
    }

    private IEnumerator DisplayTextSingle(string text)
    {
        dialogueUITypewriterEffect.Run(text, enemyDialogueTextField);
        while (dialogueUITypewriterEffect.IsRunning)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        isEnemySpeaking.Value = false;
        Hide();
    }

    public void ClearText()
    {
        enemyDialogueTextField.text = "";
    }

    public void Hide()
    {
        enemyDialogueTextField.enabled = false;
        backing.SetActive(false);
    }

    public void Show()
    {
        enemyDialogueTextField.enabled = true;
        backing.SetActive(true);
    }

    // TODO add support for DialogueObjects?
}
