using System.Collections;
using System.Collections.Generic;
using OpenAI;
using UnityEngine;

[CreateAssetMenu(menuName = "Talk/Chat Message Objects Tracker")]
public class ChatMessageObjectsTracker : ScriptableObject
{
    [SerializeField] private List<ChatMessageObject> currChatMessageObjects;
    public List<ChatMessageObject> CurrChatMessageObjects => currChatMessageObjects;

    public void AddChatMessage(ChatMessageObject chatMessageObject)
    {
        currChatMessageObjects.Add(chatMessageObject);
    }

    public void AddChatMessage(ChatMessage chatMessage)
    {
        currChatMessageObjects.Add(new ChatMessageObject(chatMessage.Role, chatMessage.Content));
    }

    public void ClearAllMessages()
    {
        currChatMessageObjects = new();
    }

    public void RemoveLastMessage()
    {
        currChatMessageObjects.RemoveAt(currChatMessageObjects.Count - 1);
    }

    public List<ChatMessage> GetChatMessages()
    {
        List<ChatMessage> ans = new();
        foreach (ChatMessageObject chatMessageObject in currChatMessageObjects)
        {
            ans.Add(chatMessageObject.ToChatMessage());
        }
        return ans;
    }
}
