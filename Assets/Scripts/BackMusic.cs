using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackMusic : MonoBehaviour
{
    private AudioSource audioSource;
    
    private static BackMusic instance;
    public static BackMusic Instance => instance;

    void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
        MusicData data = GameDataMgr.Instance.currentGameMusicData;
        SetVolume(data.MusicVolume);
        SetMute(data.isMusicOpen);
    }
    public void SetVolume(float value)
    {
        audioSource.volume = value;
    }

    public void SetMute(bool flag)
    {
        audioSource.mute = !flag;
    }
}
