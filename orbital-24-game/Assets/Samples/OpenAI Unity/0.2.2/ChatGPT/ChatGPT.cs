using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace OpenAI
{
    public class ChatGPT : MonoBehaviour
    {
        [SerializeField] private InputField inputField;
        [SerializeField] private Button button;
        [SerializeField] private ScrollRect scroll;
        
        [SerializeField] private RectTransform sent;
        [SerializeField] private RectTransform received;

        private float height;
        private OpenAIApi openai = new OpenAIApi();

        private List<ChatMessage> messages = new List<ChatMessage>();

        private void Start()
        {
            var sysMsg = new ChatMessage()
            {
                Role = "system",
                Content = @"Roleplay as a goblin, named Goblin Guard. You are hot-tempered but also extremely cowardly. 
You are employed by the Goblin Leader to guard the sole jail cell, located in the deepest depths of the Goblin Cave. 
The following conversation takes place after you spot Player walking out of his jail cell. You are fighting the player right now.
You are intimidated by the player's towering presence, and you are on the brink of running away.
Your responses should always be under 10 words long. 
Always speak in the first person. 
Answer in JSON format. Always answer with your current emotion."
+ "\nYour available emotions are {\"scared\", \"terrified\", \"angry\"}"
            };
            sysMsg.Content = sysMsg.Content.Trim();
            AppendMessage(sysMsg);
            messages.Add(sysMsg);
            var startingMsg = new ChatMessage()
            {
                Role = "assistant",
                Content = "{\"response\":\"Go back! Cell!\",\"emotion\":\"angry\"}"
            };
            messages.Add(startingMsg);
            AppendMessage(startingMsg);
            button.onClick.AddListener(SendReply);
        }

        private string ChatMessageToString(ChatMessage chatMessage)
        {
            return "Role: " + chatMessage.Role + ", Message: " + chatMessage.Content;
        }

        private void AppendMessage(ChatMessage message)
        {
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);

            var item = Instantiate(message.Role == "user" ? sent : received, scroll.content);
            item.GetChild(0).GetChild(0).GetComponent<Text>().text = message.Content;
            item.anchoredPosition = new Vector2(0, -height);
            LayoutRebuilder.ForceRebuildLayoutImmediate(item);
            height += item.sizeDelta.y;
            scroll.content.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, height);
            scroll.verticalNormalizedPosition = 0;
        }

        private async void SendReply()
        {
            var newMessage = new ChatMessage()
            {
                Role = "user",
                Content = inputField.text
            };
            
            AppendMessage(newMessage);
            
            messages.Add(newMessage);
            
            button.enabled = false;
            inputField.text = "";
            inputField.enabled = false;
            Debug.Log("----------");
            foreach (ChatMessage msg in messages)
            {
                Debug.Log(ChatMessageToString(msg));
            }
            // Complete the instruction
            var completionResponse = await openai.CreateChatCompletion(new CreateChatCompletionRequest()
            {
                Model = "gpt-4o-mini",
                Messages = messages,
                ResponseFormat = new ResponseFormat
                {
                    Type = ResponseType.JsonObject
                },
                Temperature = 0.6f
            });

            if (completionResponse.Choices != null && completionResponse.Choices.Count > 0)
            {
                var message = completionResponse.Choices[0].Message;
                message.Content = message.Content.Trim();
                message.Content = message.Content;
                
                messages.Add(message);
                AppendMessage(message);
                Debug.Log(ChatMessageToString(message));
            }
            else
            {
                Debug.LogWarning("No text was generated from this prompt.");
            }

            button.enabled = true;
            inputField.enabled = true;
            Debug.Log("----------");
        }
    }
}
