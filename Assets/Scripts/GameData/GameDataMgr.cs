
using UnityEngine;
using UnityEngine.XR;

public class GameDataMgr
{
    public static readonly int LeftOrRight = Animator.StringToHash("LeftOrRight");
    
    //游戏基本数据
    public MusicData currentGameMusicData;
    public PlayerData currentPlayerData;
    public HeroDataContainer allHeroData;
    
    //选择面板的数据
    public Transform ChooseHeroPos;
    public HeroData CurrentChooseHeroData;
    public GameObject CurrentChooseHeroObj;
    public int ChooseHeroIndex = 1;
    
    private static GameDataMgr instance = new();
    public static GameDataMgr Instance => instance;
    
    private GameDataMgr()
    {
        ChooseHeroPos = GameObject.Find("HeroPos").transform;
        
        currentGameMusicData = BinaryDateMgr.Instance.LoadDate<MusicData>("MusciData");
        if (currentGameMusicData == null) currentGameMusicData = new MusicData();
        currentPlayerData = BinaryDateMgr.Instance.LoadDate<PlayerData>("PlayerData");
        if (currentPlayerData == null) currentPlayerData = new PlayerData();
        
        //读取所有英雄的数据
        allHeroData = BinaryDateMgr.Instance.GetExcelDataTable<HeroDataContainer>();
    }

    public void SaveMusicData()
    {
        BinaryDateMgr.Instance.SaveDate(currentGameMusicData, "MusciData");
    }
    
    public bool UpdateChooseHero()
    {
        if (ChooseHeroIndex < 1) ChooseHeroIndex = allHeroData.dictionary.Count;
        if (ChooseHeroIndex > allHeroData.dictionary.Count) ChooseHeroIndex = 1;
        if(CurrentChooseHeroObj != null) GameObject.Destroy(CurrentChooseHeroObj);
        CurrentChooseHeroData = allHeroData.dictionary[ChooseHeroIndex];
        CurrentChooseHeroObj = GameObject.Instantiate(Resources.Load<GameObject>(CurrentChooseHeroData.res), ChooseHeroPos.position, ChooseHeroPos.rotation);
        return currentPlayerData.LockHero.Contains(ChooseHeroIndex);
    }
}
