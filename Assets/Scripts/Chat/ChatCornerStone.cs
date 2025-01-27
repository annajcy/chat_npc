using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChatConerStone
{
    public string name;
    public string worldView;
    public string personality;
    public string background;
    public string drive;
    public string goal;
    public string mood;

    public string GetCornerStonePrompt()
    {
        return "你是一个游戏npc" + "\n" +
               "你的名字是：" + name + "\n" +
               "你的性格是：" + personality + "\n" +
               "你生活在这样的一个世界里：" + worldView + "\n" +
               "你有着这样的背景：" + background + "\n" +
               "你的人生驱动力是：" + drive + "\n" +
               "你现在的心情怎么样: " + mood + "\n" +
               "你现在要做些什么：" + goal + "\n";
    }
}