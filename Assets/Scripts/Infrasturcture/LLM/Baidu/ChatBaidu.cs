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
        public List<RequestMessage> messages = new List<RequestMessage>();//���͵���Ϣ
    }

    [Serializable]
    private class RequestMessage
    {
        public string role = string.Empty;//��ɫ
        public string content = string.Empty;//�Ի�����
        public RequestMessage() { }
        public RequestMessage(string role,string content)
        {
            this.role = role;
            this.content = content;
        }
    }

    //���յ�����
    [Serializable]
    private class ResponseData
    {
        public string id = string.Empty;//���ֶԻ���id
        public int created;
        public int sentence_id;//��ʾ��ǰ�Ӿ����š�ֻ������ʽ�ӿ�ģʽ�»᷵�ظ��ֶ�
        public bool is_end;//��ʾ��ǰ�Ӿ��Ƿ������һ�䡣ֻ������ʽ�ӿ�ģʽ�»᷵�ظ��ֶ�
        public bool is_truncated;//��ʾ��ǰ�Ӿ��Ƿ������һ�䡣ֻ������ʽ�ӿ�ģʽ�»᷵�ظ��ֶ�
        public string result = string.Empty;//���ص��ı�
        public bool need_clear_history;//��ʾ�û������Ƿ���ڰ�ȫ
        public int ban_round;//��need_clear_historyΪtrueʱ�����ֶλ��֪�ڼ��ֶԻ���������Ϣ������ǵ�ǰ���⣬ban_round=-1
        public Usage usage = new Usage();//tokenͳ����Ϣ��token�� = ������+������*1.3 
    }

    [Serializable]
    private class Usage
    {
        public int prompt_tokens;//����tokens��
        public int completion_tokens;//�ش�tokens��
        public int total_tokens;//tokens����
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
