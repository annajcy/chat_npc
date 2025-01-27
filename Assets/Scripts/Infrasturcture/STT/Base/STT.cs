using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class STT : MonoBehaviour
{
    public string url;
    public string accessToken;
    public abstract void SpeechToTextAsync(AudioClip clip, UnityAction<string> callback);
}
