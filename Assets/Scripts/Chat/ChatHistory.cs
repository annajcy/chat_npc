using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatHistory : MonoBehaviour
{
    public int capacity = 50;
    public List<ChatData> chatHistoryList = new List<ChatData>();
    public int ChatCount => chatHistoryList.Count;
    private string chatHistorySummary = String.Empty;

    public string ChatHistorySummary
    {
        get
        {
            if (chatHistorySummary == "") return "你还没有和玩家对过话";
            return chatHistorySummary;
        }

        set => chatHistorySummary = value;
    }

    public void AddChatData(ChatData chatData)
    {
        if (ChatCount < capacity)
            chatHistoryList.Add(chatData);
        else
        {
            chatHistoryList.Clear();
            UpdateHistorySummary();
        }
    }

    public string GetChatHistorySummary()
    {
        return "你和玩家先前对话历史的总结：" + ChatHistorySummary + "\n";
    }

    public string GetUpdateChatHistorySummaryPrompt()
    {
        return "这是原先的历史对话总结：" + ChatHistorySummary + "\n" +
               "这是目前新增的对话: " + GetConversation() + "\n" +
               "请你帮我更新一下历史对话的总结";
    }

    public String GetConversation()
    {
        string result = String.Empty;
        foreach (var chatData in chatHistoryList)
            result += chatData;
        return result;
    }

    private void UpdateHistorySummary()
    {
        ChatCore.Instance().model.RequestAsync(GetUpdateChatHistorySummaryPrompt(),
            summary => chatHistorySummary = summary);
    }

}