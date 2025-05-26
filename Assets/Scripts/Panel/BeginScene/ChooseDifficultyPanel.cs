using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseDifficultyPanel : BasePanel
{
    public Button startButton;
    public Button backButton;
    public Button leftButton;
    public Button rightButton;
    public Text difficultyText;
    public Image difficultyImage;

    private Dictionary<int,DifficultyData> difficultyData;
    private int currentChooseIndex = 1;
    
    public override void PanelInit()
    {
        difficultyData = GameDataMgr.Instance.difficultyData.dictionary;
        UpdateText();
        startButton.onClick.AddListener(()=>
        {
            PanelManager.Instance.HidePanel<ChooseDifficultyPanel>();
            GameDataMgr.Instance.SceneLoad(difficultyData[currentChooseIndex].sceneName);
            PanelManager.Instance.ShowPanel<GamePanel>();
        });
        backButton.onClick.AddListener(()=>
        {
            PanelManager.Instance.HidePanel<ChooseDifficultyPanel>();
            PanelManager.Instance.ShowPanel<ChooseHeroPanel>();
        });
        leftButton.onClick.AddListener(()=>
        {
            currentChooseIndex--;
            if (currentChooseIndex < 1) currentChooseIndex = difficultyData.Count;
            UpdateText();
        });
        rightButton.onClick.AddListener(()=>
        {
            currentChooseIndex++;
            if (currentChooseIndex > difficultyData.Count) currentChooseIndex = 1;
            UpdateText();
        });
    }

    void UpdateText()
    {
        difficultyImage.sprite = Resources.Load<Sprite>("Image/Difficulty" + currentChooseIndex);
        difficultyText.text = $"名称：{difficultyData[currentChooseIndex].name}\n\n描述：{difficultyData[currentChooseIndex].tips}";
    }
}
