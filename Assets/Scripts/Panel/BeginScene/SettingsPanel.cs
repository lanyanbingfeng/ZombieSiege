using UnityEngine.UI;
public class SettingsPanel : BasePanel
{
    public Button quitButton;
    public Toggle musicToggle;
    public Toggle soundToggle;
    public Slider musicSlider;
    public Slider soundSlider;
    public Text musicText;
    public Text soundText;
    public override void PanelInit()
    {
        //初始化设置面板
        MusicData data = GameDataMgr.Instance.currentGameMusicData;
        musicToggle.isOn = data.isMusicOpen;
        soundToggle.isOn = data.isSoundOpen;
        musicSlider.value = data.MusicVolume;
        soundSlider.value = data.SoundVolume;
        musicText.text = musicSlider.value * 100 + "%";
        soundText.text = soundSlider.value * 100 + "%";
        quitButton.onClick.AddListener(() =>
        {
            PanelManager.Instance.HidePanel<SettingsPanel>();
            PanelManager.Instance.ShowPanel<BeginPanel>();
        });
        musicToggle.onValueChanged.AddListener((flag)=>
        {
            BackMusic.Instance.SetMute(flag);
            GameDataMgr.Instance.currentGameMusicData.isMusicOpen = flag;
        });
        soundToggle.onValueChanged.AddListener((flag)=>
        {
            GameDataMgr.Instance.currentGameMusicData.isSoundOpen = flag;
        });
        musicSlider.onValueChanged.AddListener((value)=>
        {
            BackMusic.Instance.SetVolume(value);
            GameDataMgr.Instance.currentGameMusicData.MusicVolume = value;
            musicText.text = (value * 100).ToString("0.0") + "%";
        });
        soundSlider.onValueChanged.AddListener((value)=>
        {
            GameDataMgr.Instance.currentGameMusicData.SoundVolume = value;
            soundText.text = (value * 100).ToString("0.0") + "%";
        });
    }
}
