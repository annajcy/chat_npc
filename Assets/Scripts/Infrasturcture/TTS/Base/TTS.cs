using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class TTS : MonoBehaviour
{
    public string url;
    public string accessToken;
    public abstract void TextToSpeechAsync(string message, UnityAction<AudioClip, string> callBack);
}
