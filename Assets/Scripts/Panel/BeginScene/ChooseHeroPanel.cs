using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseHeroPanel : BasePanel
{
    public Button LeftButton;
    public Button RightButton;
    public Button BackButton;
    public Button StartGameButton;
    public Button LockHeroButton;
    
    public Text HaveMoneyText;
    public Text LockHeroMoneyText;
    public Text HeroTipsText;
    
    public override void PanelInit()
    {
        UpdateText(GameDataMgr.Instance.UpdateChooseHero());
        LeftButton.onClick.AddListener(()=>
        {
            GameDataMgr.Instance.ChooseHeroIndex--;
            UpdateText(GameDataMgr.Instance.UpdateChooseHero());
        });
        RightButton.onClick.AddListener(()=>
        {
            GameDataMgr.Instance.ChooseHeroIndex++;
            UpdateText(GameDataMgr.Instance.UpdateChooseHero());
        });
        BackButton.onClick.AddListener(()=>
        {
            PanelManager.Instance.HidePanel<ChooseHeroPanel>();
            Camera.main.GetComponent<CameraAnimator>().TurnLeftOrRight(false, () =>
            {
                PanelManager.Instance.ShowPanel<BeginPanel>();
            });
        });
        StartGameButton.onClick.AddListener(()=>
        {
            PanelManager.Instance.HidePanel<ChooseHeroPanel>();
            PanelManager.Instance.ShowPanel<ChooseDifficultyPanel>();
        });
        LockHeroButton.onClick.AddListener(() =>
        {
            PlayerData currentPlayerData = GameDataMgr.Instance.currentPlayerData;
            
            int currentHaveMoney = currentPlayerData.HaveMoney;
            int currentLockHeroMoney = int.Parse(LockHeroMoneyText.text);
            string PromptContent;

            if (currentHaveMoney > currentLockHeroMoney)
            {
                currentPlayerData.HaveHero.Add(GameDataMgr.Instance.ChooseHeroIndex);
                currentPlayerData.HaveMoney -= currentLockHeroMoney;
                PromptContent = "解锁成功";
                GameDataMgr.Instance.SavePlayerData();
            }
            else PromptContent = "金币不足";
            //显示面板并修改提示信息
            PanelManager.Instance.ShowPanel<PromptPanel>().UpdateContent(PromptContent);
            UpdateText(GameDataMgr.Instance.UpdateChooseHero());
        });
    }
    //刷新文本数据
    private void UpdateText(bool isLock = false)
    {
        HaveMoneyText.text= GameDataMgr.Instance.currentPlayerData.HaveMoney.ToString();
        LockHeroMoneyText.text = GameDataMgr.Instance.CurrentChooseHeroData.lockMoney;
        HeroTipsText.text = GameDataMgr.Instance.CurrentChooseHeroData.tips;

        if (isLock)
        {
            StartGameButton.gameObject.SetActive(true);
            LockHeroButton.gameObject.SetActive(false);
        }
        else
        {
            StartGameButton.gameObject.SetActive(false);
            LockHeroButton.gameObject.SetActive(true);
        }
    }
}
