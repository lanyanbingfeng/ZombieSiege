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
        quitButton.onClick.AddListener(() =>
        {
            
        });
        musicToggle.onValueChanged.AddListener((flag)=>
        {
            
        });
        soundToggle.onValueChanged.AddListener((flag)=>
        {
            
        });
        musicSlider.onValueChanged.AddListener((value)=>
        {
            
        });
        soundSlider.onValueChanged.AddListener((value)=>
        {
            
        });
    }
}
