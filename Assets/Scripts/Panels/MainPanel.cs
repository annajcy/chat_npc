using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : BasePanel
{
    public Button chatButton;

    private void Start()
    {
        chatButton.onClick.AddListener(OnChatButtonClicked);
    }

    private void OnDestroy()
    {
        chatButton.onClick.RemoveListener(OnChatButtonClicked);
    }

    private void OnChatButtonClicked()
    {
        PanelManager.Instance().GetPanel<MainPanel>().Hide();
        PanelManager.Instance().GetPanel<ChatPanel>().Show();
    }
}
