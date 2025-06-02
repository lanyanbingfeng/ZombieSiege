
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : BasePanel
{
    public Button yesButton;
    public Text rewardText; //奖励
    public override void PanelInit()
    {
        rewardText.text = PanelManager.Instance.GetPanel<GamePanel>().GoldText.text.Substring(0, PanelManager.Instance.GetPanel<GamePanel>().GoldText.text.Length - 1);
        yesButton.onClick.AddListener(() =>
        {
            PanelManager.Instance.HidePanel<GamePanel>();
            PanelManager.Instance.HidePanel<GameOverPanel>();
            GameDataMgr.Instance.SceneLoad("BeginScene");
            GameDataMgr.Instance.currentPlayerData.HaveMoney += int.Parse(rewardText.text);
            GameDataMgr.Instance.SavePlayerData();
        });
    }

    public override void Show()
    {
        base.Show();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
