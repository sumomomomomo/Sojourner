using UnityEngine;
using UnityEngine.Events;

public class DialogueUIResponseSelectionHandler : MonoBehaviour
{
    [SerializeField] private GameObject selectedImage;
    [SerializeField] private IntVariable currSelectedResponseIndex;
    private UnityAction onPickedResponse;
    private int thisIndex = -1;

    // Update is called once per frame
    void Update()
    {
        if (currSelectedResponseIndex.Value == thisIndex)
        {
            selectedImage.SetActive(true);
        }
        else
        {
            selectedImage.SetActive(false);
        }
    }

    public void SetIndex(int index)
    {
        thisIndex = index;
    }

    public void SetOnPickedResponse(UnityAction unityAction)
    {
        onPickedResponse = unityAction;
    }

    public void OnPickedResponse()
    {
        onPickedResponse.Invoke();
    }
}
