using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using UnityEngine.Serialization;

public class ChatBaidu : LLM
{
    public ModelType modelType = ModelType.Qianfan_Chinese_Llama_2_7B;

    public override void RequestAsync(string message, UnityAction<string> callback)
    {
        StartCoroutine(Request(message, callback));
    }

    public IEnumerator Request(string requestWord, UnityAction<string> callback)
    {
        string postUrl = GetEndPointUrl() + "?access_token=" + accessToken;
        RequestData requestData = new RequestData
        {
            messages = new List<RequestMessage>() { new RequestMessage("user", requestWord) }
        };
        Debug.Log("posturl" + postUrl);
        var request = new UnityWebRequest(postUrl, "POST");
        string jsonData = JsonUtility.ToJson(requestData);
        Debug.Log("chatQuery" + jsonData);
        byte[] data = System.Text.Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(data);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.responseCode == 200)
        {
            string message = request.downloadHandler.text;
            Debug.Log("res" + message);
            ResponseData response = JsonUtility.FromJson<ResponseData>(message);
            callback(response.result);
        }
    }

    private string GetEndPointUrl()
    {
        return url + "/" + CheckModelType(modelType);
    }

    private string CheckModelType(ModelType type)
    {
        if (type == ModelType.ERNIE_Bot)
            return "completions";

        if (type == ModelType.ERNIE_Bot_turbo)
            return "eb-instant";

        if (type == ModelType.BLOOMZ_7B)
            return "bloomz_7b1";

        if (type == ModelType.Qianfan_BLOOMZ_7B_compressed)
            return "qianfan_bloomz_7b_compressed";

        if (type == ModelType.ChatGLM2_6B_32K)
            return "chatglm2_6b_32k";

        if (type == ModelType.Llama_2_7B_Chat)
            return "llama_2_7b";

        if (type == ModelType.Llama_2_13B_Chat)
            return "llama_2_13b";

        if (type == ModelType.Llama_2_70B_Chat)
            return "llama_2_70b";

        if (type == ModelType.Qianfan_Chinese_Llama_2_7B)
            return "qianfan_chinese_llama_2_7b";

        if (type == ModelType.AquilaChat_7B)
            return "aquilachat_7b";

        return "unknown LLM";
    }


    [Serializable]
    private class RequestData
    {
        public List<RequestMessage> messages = new List<RequestMessage>();//发送的消息
    }

    [Serializable]
    private class RequestMessage
    {
        public string role = string.Empty;//角色
        public string content = string.Empty;//对话内容
        public RequestMessage() { }
        public RequestMessage(string role,string content)
        {
            this.role = role;
            this.content = content;
        }
    }

    //接收的数据
    [Serializable]
    private class ResponseData
    {
        public string id = string.Empty;//本轮对话的id
        public int created;
        public int sentence_id;//表示当前子句的序号。只有在流式接口模式下会返回该字段
        public bool is_end;//表示当前子句是否是最后一句。只有在流式接口模式下会返回该字段
        public bool is_truncated;//表示当前子句是否是最后一句。只有在流式接口模式下会返回该字段
        public string result = string.Empty;//返回的文本
        public bool need_clear_history;//表示用户输入是否存在安全
        public int ban_round;//当need_clear_history为true时，此字段会告知第几轮对话有敏感信息，如果是当前问题，ban_round=-1
        public Usage usage = new Usage();//token统计信息，token数 = 汉字数+单词数*1.3 
    }

    [Serializable]
    private class Usage
    {
        public int prompt_tokens;//问题tokens数
        public int completion_tokens;//回答tokens数
        public int total_tokens;//tokens总数
    }

    public enum ModelType
    {
        ERNIE_Bot,
        ERNIE_Bot_turbo,
        BLOOMZ_7B,
        Qianfan_BLOOMZ_7B_compressed,
        ChatGLM2_6B_32K,
        Llama_2_7B_Chat,
        Llama_2_13B_Chat,
        Llama_2_70B_Chat,
        Qianfan_Chinese_Llama_2_7B,
        AquilaChat_7B,
    }

}
