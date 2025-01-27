using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class ChatCore : SingletonMono<ChatCore>
{
    public LLM model;
    public TTS tts;
    public STT stt;
}
