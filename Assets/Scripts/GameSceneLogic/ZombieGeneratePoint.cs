using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public enum Difficulty
{
    Easy, //僵尸ID范围 1~40
    Medium, // 1~61
    Hard, // 1~61
}

public class ZombieGeneratePoint : MonoBehaviour
{
    public Difficulty difficulty;
    private float time;

    private int currentWaveNumber = 1;
    private int deployTime = 1; //玩家部署时间
    private int zombieNumber; //每波僵尸生成数量
    private float intervalTime; //每波间隔时间
    private float zombieInterval = 3; //每只间隔时间
    private void Start()
    {
        PanelManager.Instance.GetPanel<GamePanel>().UpdateWaveNumber(currentWaveNumber);
        StartCoroutine(deployTimeOver());
        switch (difficulty)
        {
            case Difficulty.Easy:
                zombieNumber = 5;
                intervalTime = 8;
                break;
            case Difficulty.Medium:
                zombieNumber = 10;
                intervalTime = 6;
                break;
            case Difficulty.Hard:
                zombieNumber = 15;
                intervalTime = 4;
                break;
        }
    }
    //部署时间结束
    IEnumerator deployTimeOver()
    {
        float currentTime = Time.time;
        while (Time.time - currentTime < deployTime)
        {
            yield return null;
        }
        StartCoroutine(intervalTimeOver());
    }
    //每波僵尸刷新间隔时间结束
    IEnumerator intervalTimeOver()
    {
        float currentTime = Time.time;
        while (Time.time - currentTime < intervalTime)
        {
            yield return null;
        }
        StartCoroutine(UpdateZombie());
    }
    //开始刷新僵尸
    IEnumerator UpdateZombie()
    {
        int currentZombieNumber = 0;
        while (currentZombieNumber != zombieNumber)
        {
            yield return new WaitForSeconds(zombieInterval);
            int index = 0;
            int range;
            switch (difficulty)
            {
                case Difficulty.Easy:
                    index = Random.Range(1, 41);
                    break;
                case Difficulty.Medium:
                    range = Random.Range(1, 101);
                    if (range < 70) index = Random.Range(1, 41);
                    else index = Random.Range(41,62);
                    break;
                case Difficulty.Hard:
                    range = Random.Range(1, 101);
                    if (range > 70) index = Random.Range(1, 41);
                    else index = Random.Range(41,62);
                    break;
            }
            ZombieData zombieData = GameDataMgr.Instance.allZombieData.dictionary[index];
            GameObject zombie = Instantiate(Resources.Load<GameObject>(zombieData.res), transform.position, Quaternion.identity);
            zombie.AddComponent<ZombieObj>().zombieData = zombieData;
            zombie.GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>(zombieData.animatorRes);
            currentZombieNumber++;
        }
        currentWaveNumber++;
        PanelManager.Instance.GetPanel<GamePanel>().UpdateWaveNumber(currentWaveNumber);
        StartCoroutine(deployTimeOver());
    }
}
