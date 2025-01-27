using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Serialization;


public class BaiduTextToSpeech : TTS
{
    public PostDataSetting postDataSetting;

    public override void TextToSpeechAsync(string message, UnityAction<AudioClip, string> callBack)
    {
        StartCoroutine(GetSpeech(message, callBack));
    }

    private IEnumerator GetSpeech(string message, UnityAction<AudioClip, string> callback)
    {
        var postUrl = url;
        var postParams = new Dictionary<string, string>
        {
            { "tex", message },
            { "tok", accessToken },
            { "cuid", SystemInfo.deviceUniqueIdentifier },
            { "ctp", postDataSetting.clientType },
            { "lan", postDataSetting.language },
            { "spd", postDataSetting.speed },
            { "pit", postDataSetting.pitch },
            { "vol", postDataSetting.volume },
            { "per", SetSpeeker(postDataSetting.peron) },
            { "aue", postDataSetting.format }
        };

        //拼接参数到链接里
        int i = 0;
        foreach (var item in postParams)
        {
            postUrl += i != 0 ? "&" : "?";
            postUrl += item.Key + "=" + item.Value;
            i++;
        }

        //合成音频
        UnityWebRequest speechRequest = UnityWebRequestMultimedia.GetAudioClip(postUrl, AudioType.WAV);
        yield return speechRequest.SendWebRequest();
        if (speechRequest.error == null)
        {
            var type = speechRequest.GetResponseHeader("Content-Type");
            if (type.Contains("audio"))
            {
                var clip = DownloadHandlerAudioClip.GetContent(speechRequest);
                callback(clip, message);
            }
            else
            {
                var response = speechRequest.downloadHandler.data;
                string msgBack = System.Text.Encoding.UTF8.GetString(response);
                UnityEngine.Debug.LogError(msgBack);
            }

        }

    }
    //基础音库:度小宇=1，度小美=0，度逍遥（基础）=3，度丫丫=4
    /// 精品音库:度逍遥（精品）=5003，度小鹿=5118，度博文=106，度小童=110，度小萌=111，度米朵=103，度小娇=5
    private string SetSpeeker(SpeakerRole role)
    {
        if (role == SpeakerRole.度小宇) return "1";
        if (role == SpeakerRole.度小美) return "0";
        if (role == SpeakerRole.度逍遥) return "3";
        if (role == SpeakerRole.度丫丫) return "4";
        if (role == SpeakerRole.JP度小娇) return "5";
        if (role == SpeakerRole.JP度逍遥) return "5003";
        if (role == SpeakerRole.JP度小鹿) return "5118";
        if (role == SpeakerRole.JP度博文) return "106";
        if (role == SpeakerRole.JP度小童) return "110";
        if (role == SpeakerRole.JP度小萌) return "111";
        if (role == SpeakerRole.JP度米朵) return "5";

        return "0";//默认为度小美
    }


    #region 数据格式定义



    /// <summary>
    /// 语音合成的配置信息
    /// </summary>
    [System.Serializable]
    public class PostDataSetting
    {
        public string clientType = "1";
        public string language = "zh";
        public string speed = "5";
        public string pitch = "5";
        public string volume = "5";
        public string format = "6";
        public SpeakerRole peron = SpeakerRole.度小美;
    }
    /// <summary>
    /// 可选声音
    /// </summary>
    public enum SpeakerRole
    {
        度小宇,
        度小美,
        度逍遥,
        度丫丫,
        JP度逍遥,
        JP度小鹿,
        JP度博文,
        JP度小童,
        JP度小萌,
        JP度米朵,
        JP度小娇
    }

    /// <summary>
    /// 语音合成结果
    /// </summary>
    public class SpeechResponse
    {
        public int error_index;
        public string error_msg;
        public string sn;
        public int idx;
        public bool Success
        {
            get { return error_index == 0; }
        }
        public AudioClip clip;
    }


    #endregion

}
