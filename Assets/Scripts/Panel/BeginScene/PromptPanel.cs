using UnityEngine.UI;

public class PromptPanel : BasePanel
{
    public Button YesButton;
    public Text Content;
    public override void PanelInit()
    {
        YesButton.onClick.AddListener(() =>
        {
            PanelManager.Instance.HidePanel<PromptPanel>();
        });
    }

    public void UpdateContent(string content)
    {
        Content.text = content;
    }
}
