using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class VoiceInputs : MonoBehaviour
{
    public int recordingLength = 10;
    public AudioClip recording;

    public void StartRecordAudio()
    {
        Debug.Log("rec started");
        recording = Microphone.Start(null, false, recordingLength, 16000);
    }

    public void StopRecordAudio(UnityAction<AudioClip> callback)
    {
        Debug.Log("rec ended");
        Microphone.End(null);
        callback(recording);
    }

}
