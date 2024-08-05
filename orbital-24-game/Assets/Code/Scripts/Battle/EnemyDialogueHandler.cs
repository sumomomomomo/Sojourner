using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyDialogueHandler : MonoBehaviour
{
    [SerializeField] private TMP_Text enemyDialogueTextField;
    [SerializeField] private GameObject backing;
    [SerializeField] private DialogueUITypewriterEffect dialogueUITypewriterEffect;
    [SerializeField] private BoolVariable isEnemySpeaking;
    private Coroutine speakCoroutine;
    public void DisplayTextExpire(string text, float duration)
    {
        if (speakCoroutine != null)
            StopCoroutine(speakCoroutine);
        Show();
        isEnemySpeaking.Value = true;
        speakCoroutine = StartCoroutine(DisplayTextSingleExpire(text, duration));
    }

    private IEnumerator DisplayTextSingleExpire(string text, float duration)
    {
        dialogueUITypewriterEffect.Run(text, enemyDialogueTextField);
        while (dialogueUITypewriterEffect.IsRunning)
        {
            yield return null;
        }
        yield return new WaitForSeconds(duration);
        isEnemySpeaking.Value = false;
        Hide();
    }
    public void DisplayText(string text)
    {
        if (speakCoroutine != null)
            StopCoroutine(speakCoroutine);
        Show();
        isEnemySpeaking.Value = true;
        speakCoroutine = StartCoroutine(DisplayTextSingle(text));
    }

    private IEnumerator DisplayTextSingle(string text)
    {
        dialogueUITypewriterEffect.Run(text, enemyDialogueTextField);
        while (dialogueUITypewriterEffect.IsRunning)
        {
            yield return null;
        }
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || Keyboard.current[Key.Space].wasPressedThisFrame);
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

    public void KillCoroutine()
    {
        if (speakCoroutine != null)
            StopCoroutine(speakCoroutine);
    }

    // TODO add support for DialogueObjects?
}
