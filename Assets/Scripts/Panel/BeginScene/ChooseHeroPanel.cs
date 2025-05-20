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
            SceneManager.LoadSceneAsync("GameScene");
        });
        LockHeroButton.onClick.AddListener(() =>
        {
            
        });
    }
    //刷新文本数据
    private void UpdateText(bool isLock = false)
    {
        HaveMoneyText.text= GameDataMgr.Instance.currentPlayerData.HaveMoney.ToString();
        LockHeroMoneyText.text = "$:" + GameDataMgr.Instance.CurrentChooseHeroData.lockMoney;
        HeroTipsText.text = GameDataMgr.Instance.CurrentChooseHeroData.tips;
        if (isLock) LockHeroButton.gameObject.SetActive(false);
        else LockHeroButton.gameObject.SetActive(true);
    }
}
