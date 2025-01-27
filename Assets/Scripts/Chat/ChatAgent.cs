using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ChatAgent : MonoBehaviour
{
    public ChatConerStone chatCornerStone;
    public ChatHistory chatHistory;

    [ContextMenu("GetPrompt")]
    public string GetPrompt()
    {
        Debug.Log(chatCornerStone.GetCornerStonePrompt() +
                  chatHistory.GetChatHistorySummary() +
                  "想象一下，上述是你的身份。请你回答一下这句话\n" +
                  chatHistory.GetConversation() +
                  "你：");
        return chatCornerStone.GetCornerStonePrompt() +
               chatHistory.GetChatHistorySummary() +
               "想象一下，上述是你的身份。请你回答一下这句话\n" +
               chatHistory.GetConversation() +
               "你：";
    }
}