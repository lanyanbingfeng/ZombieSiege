using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public RectTransform HPImage;
    public Text HPText;
    public int MaxHpWidth = 500;
    public Text WaveNumberText; //剩余波数
    public Text GoldText;
    public Button BackButton;
    public override void PanelInit()
    {
        BackButton.onClick.AddListener(() =>
        {
            PanelManager.Instance.HidePanel<GamePanel>();
            GameDataMgr.Instance.SceneLoad("BeginScene");
        });
    }

    public void UpdateHP(int newHp)
    {
        float hpPercent = (float)newHp / GameDataMgr.Instance.MaxHp; //比例
        HPImage.sizeDelta = new Vector2(MaxHpWidth * hpPercent, HPImage.rect.height);
        HPText.text = newHp + "/100";
    }

    public void UpdateWaveNumber(int newWaveNumber)
    {
        WaveNumberText.text = newWaveNumber + "/12";
    }

    public void UpdateGold(int newGold)
    {
        GoldText.text = newGold.ToString();
    }
}
