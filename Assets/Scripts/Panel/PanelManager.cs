
using System.Collections.Generic;
using UnityEngine;

public class PanelManager
{
    private Dictionary<string,BasePanel> dicPanel = new();
    private Transform canvas;
    
    private static PanelManager instance = new ();
    public static PanelManager Instance => instance;
    private PanelManager()
    {
        GameObject canvasObj = GameObject.Instantiate(Resources.Load<GameObject>("Panels/Canvas"));
        canvas = canvasObj.transform;
        GameObject.DontDestroyOnLoad(canvasObj);
    }

    public T ShowPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (dicPanel.ContainsKey(panelName)) return dicPanel[panelName] as T;
        
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("Panels/" + panelName), canvas, false);
        T panel = panelObj.GetComponent<T>();
        panel.Show();
        dicPanel.Add(panelName, panel);
        return panel;
    }

    public void HidePanel<T>(bool isFade = true) where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (dicPanel.ContainsKey(panelName))
        {
            if (isFade)
            {
                dicPanel[panelName].Hide(() =>
                {
                    GameObject.Destroy(dicPanel[panelName].gameObject);
                    dicPanel.Remove(panelName);
                });
            }
            else
            {
                GameObject.Destroy(dicPanel[panelName].gameObject);
                dicPanel.Remove(panelName);
            }
        }
    }

    public T GetPanel<T>() where T : BasePanel
    {
        string panelName = typeof(T).Name;
        if (dicPanel.ContainsKey(panelName))
        {
            return dicPanel[panelName] as T;
        }
        return null;
    }
}
