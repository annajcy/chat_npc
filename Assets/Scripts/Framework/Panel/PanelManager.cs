using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : SingletonMono<PanelManager>
{
    public List<BasePanel> panelList = new List<BasePanel>();
    private Dictionary<string, BasePanel> panelDict = new Dictionary<string, BasePanel>();

    protected override void Awake()
    {
        base.Awake();
        RegisterPanels();
    }

    private void RegisterPanels()
    {
        foreach (var panel in panelList)
            panelDict.Add(panel.gameObject.name, panel);
    }


    public T GetPanel<T>() where T : BasePanel
    {
        string name = typeof(T).Name;
        if (!panelDict.TryGetValue(name, out var panel)) return null;
        return panel as T;
    }
}
