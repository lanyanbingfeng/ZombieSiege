using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameDataMgr
{
    public static readonly int LeftOrRight = Animator.StringToHash("LeftOrRight");
    
    //游戏基本数据
    public MusicData currentGameMusicData;
    public PlayerData currentPlayerData;
    public HeroDataContainer allHeroData;
    public DifficultyDataContainer difficultyData;
    
    //选择面板的数据
    public Transform ChooseHeroPos;
    public HeroData CurrentChooseHeroData;
    public GameObject CurrentChooseHeroObj;
    public int ChooseHeroIndex = 1;
    
    //游戏场景玩家状态
    public int MaxHp = 100; //最大生命值
    
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
        difficultyData = BinaryDateMgr.Instance.GetExcelDataTable<DifficultyDataContainer>();
    }

    public void SceneLoad(string sceneName)
    {
        SceneManager.sceneLoaded += OnSceneLoadOver;
        SceneManager.LoadScene(sceneName);
    }
    //场景加载完毕回调
    private void OnSceneLoadOver(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoadOver;
        if (scene.name == "GameScene1" || scene.name == "GameScene2" || scene.name == "GameScene3")
        {
            Transform heroPos = GameObject.Find("HeroGeneratePos").transform;
            GameObject PlayerHero = Object.Instantiate(Resources.Load<GameObject>(allHeroData.dictionary[ChooseHeroIndex].res),
                heroPos.position,
                heroPos.rotation
            );
            PlayerHero.AddComponent<PlayerObj>();
        }
        else if (scene.name == "BeginScene")
        {
            ChooseHeroPos = GameObject.Find("HeroPos").transform;
        }
    }

    public void SaveMusicData()
    {
        BinaryDateMgr.Instance.SaveDate(currentGameMusicData, "MusciData");
    }

    public void SavePlayerData()
    {
        BinaryDateMgr.Instance.SaveDate(currentPlayerData, "PlayerData");
    }
    
    public bool UpdateChooseHero()
    {
        //判断当前选择英雄的索引是否超出
        if (ChooseHeroIndex < 1) ChooseHeroIndex = allHeroData.dictionary.Count;
        if (ChooseHeroIndex > allHeroData.dictionary.Count) ChooseHeroIndex = 1;
        //如果当前有模型在场景中，删除当前选择的英雄
        if(CurrentChooseHeroObj != null) Object.Destroy(CurrentChooseHeroObj);
        //找到下一个英雄的信息
        CurrentChooseHeroData = allHeroData.dictionary[ChooseHeroIndex];
        //生成英雄
        CurrentChooseHeroObj = Object.Instantiate(Resources.Load<GameObject>(CurrentChooseHeroData.res), ChooseHeroPos.position, ChooseHeroPos.rotation);
        //查找玩家拥有的英雄列表是否有这个英雄并返回 bool
        return currentPlayerData.HaveHero.Contains(ChooseHeroIndex);
    }
}
