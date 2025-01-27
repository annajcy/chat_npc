using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ChatServer : MonoBehaviour
{
    public bool isVoiceMode = true;
    public bool isCreateVoiceMode = false;

    public ChatAgent chatAgent;
    public Animator characterAnimator;
    public AudioSource audioSource;

    private void Start()
    {
        var chatPanel = PanelManager.Instance().GetPanel<ChatPanel>();
        chatPanel.commitButton.onClick.AddListener(SendData);
    }

    private void OnDestroy()
    {
        var chatPanel = PanelManager.Instance().GetPanel<ChatPanel>();
        chatPanel.commitButton.onClick.RemoveListener(SendData);
    }

    private void SetAnimator(string param,int value)
    {
        if (characterAnimator == null) return;
        characterAnimator.SetInteger(param, value);
    }

#region ��Ϣ����

    public void SendData()
    {
        var userInputText = PanelManager.Instance().GetPanel<ChatPanel>().userInput;
        var respondText = PanelManager.Instance().GetPanel<ChatPanel>().textResponse;

        if (userInputText.text == "") return;

        if (isCreateVoiceMode) //�ϳ�����Ϊ����
        {
            ProcessResponse(userInputText.text);
            userInputText.text = "";
            return;
        }

        chatAgent.chatHistory.AddChatData(new ChatData(ChatRole.User, userInputText.text));
        //��������
        ChatCore.Instance().model.RequestAsync(chatAgent.GetPrompt(), ProcessResponse);
        userInputText.text = "";
        respondText.text = "����˼����...";

        //�л�˼������
        SetAnimator("state", 1);
    }

    private void ProcessResponse(string response)
    {
        var respondText = PanelManager.Instance().GetPanel<ChatPanel>().textResponse;

        response = response.Trim();
        respondText.text = "";
        
        Debug.Log("�յ�AI�ظ���"+ response);

        chatAgent.chatHistory.AddChatData(new ChatData(ChatRole.Assistant, response));

        if (!isVoiceMode || ChatCore.Instance().tts == null)
        {
            StartTypeWords(response);
            return;
        }

        ChatCore.Instance().tts.TextToSpeechAsync(response, PlayVoice);
    }

#endregion

#region ��������

    public bool autoSend = true;
    [SerializeField] private VoiceInputs voiceInputs;

    public void StartRecord()
    {
        voiceInputs.StartRecordAudio();
    }

    public void StopRecord()
    {
        voiceInputs.StopRecordAudio(AcceptClip);
    }

    private void AcceptClip(AudioClip audioClip)
    {
        if (ChatCore.Instance().stt == null) return;
        ChatCore.Instance().stt.SpeechToTextAsync(audioClip, ProcessText);
    }

    private void ProcessText(string messages)
    {
        var userInputText = PanelManager.Instance().GetPanel<ChatPanel>().userInput;
        userInputText.text = messages;
        if (autoSend)
            Invoke(nameof(SendData), 1f);
    }


#endregion

#region �����ϳ�

    private void PlayVoice(AudioClip audioClip, string response)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        Debug.Log("��Ƶʱ����" + audioClip.length);
        //��ʼ�����ʾ���ص��ı�
        StartTypeWords(response);
        //�л���˵������
        SetAnimator("state", 2);
    }

#endregion

#region ���������ʾ

    private float wordWaitTime = 0.2f;
    private bool writeState = false;

    private void StartTypeWords(string message)
    {
        if (message == "") return;
        writeState = true;
        StartCoroutine(SetTextPerWord(message));
    }

    private IEnumerator SetTextPerWord(string message)
    {
        var respondText = PanelManager.Instance().GetPanel<ChatPanel>().textResponse;
        int currentPos = 0;
        while (writeState)
        {
            yield return new WaitForSeconds(wordWaitTime);
            currentPos ++;
            //������ʾ������
            respondText.text = message.Substring(0, currentPos);
            writeState = currentPos < message.Length;
        }

        //�л����ȴ�����
        SetAnimator("state",0);
    }

#endregion

}
