using System;
using System.Collections;
using System.Collections.Generic;
using OpenAI;
using UnityEngine;

[Serializable]
public class ChatMessageObject
{
    [SerializeField] private string role;
    [SerializeField] [TextArea] private string content;

    public ChatMessageObject(string role, string content)
    {
        this.role = role;
        this.content = content;
    }
    public ChatMessage ToChatMessage()
    {
        return new ChatMessage()
        {
            Role = role,
            Content = content
        };
    }
}
