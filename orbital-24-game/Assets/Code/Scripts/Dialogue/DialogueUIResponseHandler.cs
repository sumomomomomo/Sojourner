using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.Events;

/// <summary>
/// Composed under DialogueUI. Handles logic of responses.
/// </summary>
public class DialogueUIResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;
    [SerializeField] private IntVariable currResponseIndex;
    [SerializeField] private GameObject spacebarWatcher;

    private DialogueUI dialogueUI;

    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
        spacebarWatcher.SetActive(false);
    }

    public void ShowResponses(DialogueResponse[] responses) 
    {
        float responseBoxHeight = 0;
        currResponseIndex.Value = 0;
        List<GameObject> generatedButtons = new();
        List<UnityAction> actions = new();

        for (int i = 0; i < responses.Length; i++)
        {
            DialogueResponse response = responses[i];
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer); // TODO change to object pool pattern
            generatedButtons.Add(responseButton);
            UnityAction currOnPickedResponse = () => OnPickedResponse(response, generatedButtons);
            actions.Add(currOnPickedResponse);
            responseButton.SetActive(true);
            responseButton.GetComponent<DialogueUIResponseSelectionHandler>().SetIndex(i);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>()
                .onClick
                .AddListener(currOnPickedResponse);

            responseBoxHeight += responseButtonTemplate.sizeDelta.y + 10;
        }
        responseBoxHeight -= 10;

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);

        spacebarWatcher.SetActive(true);
        spacebarWatcher.GetComponent<DialogueUIResponseInputHandler>().Init(actions, currResponseIndex);
    }

    private void OnPickedResponse(DialogueResponse response, List<GameObject> generatedButtons)
    {
        spacebarWatcher.SetActive(false);
        foreach (GameObject generatedButton in generatedButtons)
        {
            Destroy(generatedButton);
        }
        responseBox.gameObject.SetActive(false);

        // Show new dialogue and call associated event at the same time
        response.OnPickedResponse?.Invoke();
        dialogueUI.ShowDialogue(response.NextDialogue);
    }
}
