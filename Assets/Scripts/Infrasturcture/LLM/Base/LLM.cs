using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public abstract class LLM : MonoBehaviour
{
    public string url;
    public string accessToken;
    public abstract void RequestAsync(string message, UnityAction<string> callback);
}
