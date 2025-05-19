using UnityEngine;
using UnityEngine.UI;

public class BeginPanel : BasePanel
{
    public Button startButton;
    public Button settingsButton;
    public Button aboutButton;
    public Button quitButton;
    public override void PanelInit()
    {
        startButton.onClick.AddListener(()=>
        {
            
        });
        settingsButton.onClick.AddListener(()=>
        {
            PanelManager.Instance.HidePanel<BeginPanel>();
            PanelManager.Instance.ShowPanel<SettingsPanel>();
        });
        aboutButton.onClick.AddListener(()=>
        {
            
        });
        quitButton.onClick.AddListener(()=>
        {
            Application.Quit();
        });
    }
}