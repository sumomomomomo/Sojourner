using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// Composed under DialogueUI. Handles logic of responses.
/// </summary>
public class DialogueUIResponseHandler : MonoBehaviour
{
    [SerializeField] private RectTransform responseBox;
    [SerializeField] private RectTransform responseButtonTemplate;
    [SerializeField] private RectTransform responseContainer;

    private DialogueUI dialogueUI;

    private void Start()
    {
        dialogueUI = GetComponent<DialogueUI>();
    }

    public void ShowResponses(DialogueResponse[] responses) 
    {
        float responseBoxHeight = 0;

        for (int i = 0; i < responses.Length; i++)
        {
            DialogueResponse response = responses[i];
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.SetActive(true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>()
                .onClick
                .AddListener(() => OnPickedResponse(response));

            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
        }

        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.gameObject.SetActive(true);
    }

    private void OnPickedResponse(DialogueResponse response)
    {
        responseBox.gameObject.SetActive(false);

        // Show new dialogue and call associated event at the same time
        response.OnPickedResponse?.Invoke();
        dialogueUI.ShowDialogue(response.NextDialogue);
    }
}
