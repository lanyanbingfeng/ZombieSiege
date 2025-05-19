
using UnityEngine;
using UnityEngine.XR;

public class GameDataMgr
{
    public MusicData currentGameMusicData;
    
    private static GameDataMgr instance = new();
    public static GameDataMgr Instance => instance;
    
    private GameDataMgr()
    {
        currentGameMusicData = BinaryDateMgr.Instance.LoadDate<MusicData>("MusciData");
        if (currentGameMusicData == null) currentGameMusicData = new MusicData();
    }
}
