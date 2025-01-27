using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BaiduSpeechToText : STT
{
    public override void SpeechToTextAsync(AudioClip clip, UnityAction<string> callback)
    {
        StartCoroutine(SpeechToText(clip, callback));
    }

    private IEnumerator SpeechToText(AudioClip audioClip, UnityAction<string> callBack)
    {
        string asrResult = string.Empty;

        //处理当前录音数据为PCM16
        float[] samples = new float[audioClip.samples];
        audioClip.GetData(samples, 0);
        var samplesShort = new short[samples.Length];
        for (var index = 0; index < samples.Length; index++)
            samplesShort[index] = (short)(samples[index] * short.MaxValue);

        byte[] datas = new byte[samplesShort.Length * 2];
        Buffer.BlockCopy(samplesShort, 0, datas, 0, datas.Length);

        string postUrl = string.Format(url + "?cuid={0}&token={1}", SystemInfo.deviceUniqueIdentifier, accessToken);

        WWWForm wwwForm = new WWWForm();
        wwwForm.AddBinaryData("audio", datas);

        UnityWebRequest unityWebRequest = UnityWebRequest.Post(postUrl, wwwForm);
        unityWebRequest.SetRequestHeader("Content-Type", "audio/pcm;rate=16000");

        yield return unityWebRequest.SendWebRequest();

        if (string.IsNullOrEmpty(unityWebRequest.error))
        {
            asrResult = unityWebRequest.downloadHandler.text;
            RecognizeBackData data = JsonUtility.FromJson<RecognizeBackData>(asrResult);
            if (data.err_no == "0")
                callBack(data.result[0]);
            else
                callBack("语音识别失败");
        }
    }

    [Serializable]
    public class RecognizeBackData
    {
        public string corpus_no = string.Empty;
        public string err_msg = string.Empty;
        public string err_no = string.Empty;
        public List<string> result;
        public string sn = string.Empty;
    }

    [Serializable]
    public class TokenInfo
    {
        public string access_token = string.Empty;
    }
}
