using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    //主塔血条
    public RectTransform HPImage;
    public Text HPText;
    
    //玩家血条
    public RectTransform PlayerHPImage;
    public Text PlayerHPText;
    
    public int MaxHpWidth = 500;
    public Text WaveNumberText; //剩余波数
    public Text GoldText;
    public Button BackButton;
    
    public GameObject TurretObject;
    public override void PanelInit()
    {
        TurretObject.SetActive(false);
        BackButton.onClick.AddListener(() =>
        {
            PanelManager.Instance.HidePanel<GamePanel>();
            GameDataMgr.Instance.SceneLoad("BeginScene");
        });
    }
    
    //主塔刷新血量
    public void UpdateMainTowerHP(int newHp)
    {
        float hpPercent = (float)newHp / GameDataMgr.Instance.MainTowerHp; //比例
        HPImage.sizeDelta = new Vector2(MaxHpWidth * hpPercent, HPImage.rect.height);
        HPText.text = newHp + "/100";
    }

    public void UpdatePlayerHP(int newHp)
    {
        float hpPercent = (float)newHp / GameDataMgr.Instance.playerMaxHp; //比例
        PlayerHPImage.sizeDelta = new Vector2(MaxHpWidth * hpPercent, PlayerHPImage.rect.height);
        PlayerHPText.text = newHp + "/100";
    }

    public void UpdateWaveNumber(int newWaveNumber)
    {
        WaveNumberText.text = newWaveNumber + "/12";
    }

    public void UpdateGold(int newGold)
    {
        GoldText.text = newGold.ToString();
    }

    public override void Show()
    {
        base.Show();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
