using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class ChatPanel : BasePanel
{
    public TMP_InputField userInput;
    public TMP_Text textResponse;
    public Button voiceInputButton;
    public Button commitButton;
    public Button returnButton;

    private void Start()
    {
        returnButton.onClick.AddListener(OnReturnButtonClicked);
    }

    private void OnDestroy()
    {
        returnButton.onClick.RemoveListener(OnReturnButtonClicked);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            commitButton.onClick.Invoke();
        if (Input.GetKeyDown(KeyCode.Escape))
            returnButton.onClick.Invoke();
    }

    private void OnReturnButtonClicked()
    {
        PanelManager.Instance().GetPanel<ChatPanel>().Hide();
        PanelManager.Instance().GetPanel<MainPanel>().Show();
    }
}
