using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioToLipSample : MonoBehaviour
{
    [Tooltip("Which lip sync provider to use for viseme computation.")]
    public OVRLipSync.ContextProviders provider = OVRLipSync.ContextProviders.Enhanced;
    [Tooltip("Enable DSP offload on supported Android devices.")]
    public bool enableAcceleration = true;
    [SerializeField]private uint Context = 0;
    [SerializeField]public float gain = 1.0f;
    [SerializeField] private AudioSource m_AudioSource;

    [SerializeField]private Text m_LogText;
    /// <summary>
    /// ���ط������
    /// </summary>
    private OVRLipSync.Frame frame = new OVRLipSync.Frame();
    protected OVRLipSync.Frame Frame
    {
        get
        {
            return frame;
        }
    }

    private void Awake()
    {
        m_AudioSource = this.GetComponent<AudioSource>();
        if (Context == 0)
        {
            if (OVRLipSync.CreateContext(ref Context, provider, 0, enableAcceleration)
                != OVRLipSync.Result.Success)
            {
                Debug.LogError("OVRLipSyncContextBase.Start ERROR: Could not create" +
                    " Phoneme context.");
                return;
            }
        }
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        ProcessAudioSamplesRaw(data, channels);
    }

    /// <summary>
    /// Pass F32 PCM audio buffer to the lip sync module
    /// </summary>
    /// <param name="data">Data.</param>
    /// <param name="channels">Channels.</param>
    public void ProcessAudioSamplesRaw(float[] data, int channels)
    {
        // Send data into Phoneme context for processing (if context is not 0)
        lock (this)
        {
            if (OVRLipSync.IsInitialized() != OVRLipSync.Result.Success)
            {
                return;
            }
            var frame = this.Frame;
            OVRLipSync.ProcessFrame(Context, data, frame, channels == 2);
        }
    }

    private void Update()
    {
        if (this.Frame != null)
        {
            Logger();
            SetBlenderShapes();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartRecord();
        }
        if (Input.GetKeyUp(KeyCode.R))
        {
            StopRecord();
        }
    }

    /// <summary>
    /// �������봦����
    /// </summary>
    [SerializeField] private VoiceInputs m_VoiceInputs;
    /// <summary>
    /// ��ʼ¼��
    /// </summary>
    public void StartRecord()
    {
        m_VoiceInputs.StartRecordAudio();
    }
    /// <summary>
    /// ����¼��
    /// </summary>
    public void StopRecord()
    {
        m_VoiceInputs.StopRecordAudio(PlayRecord);
    }
    /// <summary>
    /// ����¼��
    /// </summary>
    /// <param name="_clip"></param>

    private void PlayRecord(AudioClip _clip)
    {
        m_AudioSource.clip= _clip;
        m_AudioSource.Play();
    }

    /// <summary>
    /// ��ӡʮ�ĸ����ص�Ȩ��
    /// </summary>
    private void Logger()
    {
        string seq = "";
        for (int i = 0; i < this.Frame.Visemes.Length; i++)
        {
            seq += ((OVRLipSync.Viseme)i).ToString();
            seq += ":";

            int count = (int)(50.0f * this.Frame.Visemes[i]);
            for (int c = 0; c < count; c++)
                seq += "*";

            seq += "\n";
        }
        m_LogText.text = seq;
    }
    /// <summary>
    /// ģ�͵�SkinnedMeshRenderer���
    /// </summary>
    public SkinnedMeshRenderer meshRenderer;
    /// <summary>
    /// blendshapeȨ�ر���
    /// </summary>
    public float blendWeightMultiplier = 100f; 
    /// <summary>
    /// ����ÿ�����Ͷ�Ӧ��blendershape������
    /// </summary>
    public VisemeBlenderShapeIndexMap m_VisemeIndex;

    private void SetBlenderShapes()
    {
        for (int i = 0; i < this.Frame.Visemes.Length; i++) {
            string _name= ((OVRLipSync.Viseme)i).ToString();
            int blendShapeIndex = GetBlenderShapeIndexByName(_name);
            int blendWeight = (int)(blendWeightMultiplier * this.Frame.Visemes[i]);
            if (blendShapeIndex == 999)
                continue;

            meshRenderer.SetBlendShapeWeight(blendShapeIndex, blendWeight);
        }
    }



    /// <summary>
    /// ���ж��£�����a i u e o ������
    /// </summary>
    /// <param name="_name"></param>
    /// <returns></returns>
    private int GetBlenderShapeIndexByName(string _name)
    {
        if(_name== "sil")
        {
            return 999;
        }
        if (_name == "aa") {
            return m_VisemeIndex.A;
        }
        if (_name == "ih")
        {
            return m_VisemeIndex.I;
        }
        if (_name == "E")
        {
            return m_VisemeIndex.E;
        }
        if (_name == "oh")
        {
            return m_VisemeIndex.O;
        }

        return m_VisemeIndex.U;
    }

}


