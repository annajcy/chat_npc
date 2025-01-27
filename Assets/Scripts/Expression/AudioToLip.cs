using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AudioToLip : MonoBehaviour
{
    public OVRLipSync.ContextProviders provider = OVRLipSync.ContextProviders.Enhanced;
    public bool enableAcceleration = true;
    public uint context = 0;
    public float gain = 1.0f;
    public AudioSource audioSource;
    public SkinnedMeshRenderer meshRenderer;
    public float blendWeightMultiplier = 150f;
    public VisemeBlenderShapeIndexMap visemeIndex;
    private OVRLipSync.Frame Frame { get; } = new OVRLipSync.Frame();

    private void Awake()
    {
        if (context != 0) return;
        lock (this)
        {
            if (OVRLipSync.CreateContext(ref context, provider, 0, enableAcceleration)
                == OVRLipSync.Result.Success) return;
            Debug.LogError("OVRLipSyncContextBase.Start ERROR: Could not create" +
                           " Phoneme context.");
        }
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        ProcessAudioSamplesRaw(data, channels);
    }

    private void ProcessAudioSamplesRaw(float[] data, int channels)
    {
        // Send data into Phoneme context for processing (if context is not 0)
        lock (this)
        {
            if (OVRLipSync.IsInitialized() != OVRLipSync.Result.Success) return;
            OVRLipSync.ProcessFrame(context, data, Frame, channels == 2);
        }
    }

    private void Update()
    {
        if (Frame != null)
            SetBlenderShapes();
    }


    private void SetBlenderShapes()
    {
        for (int i = 0; i < Frame.Visemes.Length; i ++)
        {
            string voice = ((OVRLipSync.Viseme)i).ToString();
            int blendShapeIndex = GetBlenderShapeIndexByName(voice);
            int blendWeight = (int)(blendWeightMultiplier * Frame.Visemes[i]);
            if (blendShapeIndex == 999)
                continue;
            meshRenderer.SetBlendShapeWeight(blendShapeIndex, blendWeight);
        }
    }

    private int GetBlenderShapeIndexByName(string voice)
    {
        if (voice == "sil") return 999;
        if (voice == "aa") return visemeIndex.A;
        if (voice == "ih") return visemeIndex.I;
        if (voice == "E") return visemeIndex.E;
        if (voice == "oh") return visemeIndex.O;
        return visemeIndex.U;
    }

}
