using UnityEngine;
using System;

[Serializable]
public class ChatData
{
    public ChatRole role;
    public string content;

    public ChatData(ChatRole role, string content)
    {
        this.role = role;
        this.content = content;
    }

    public override string ToString()
    {
        return GetRoleString(role) + ":" + content + "\n";
    }

    public static string GetRoleString(ChatRole role)
    {
        if (role == ChatRole.User) return "玩家";
        if (role == ChatRole.Assistant) return "你";
        return "unknown";
    }
}