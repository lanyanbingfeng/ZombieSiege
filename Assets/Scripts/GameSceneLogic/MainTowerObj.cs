
using UnityEngine;

public class MainTowerObj : MonoBehaviour
{
    private int hp = 100;
    public bool isDead;
    
    private static MainTowerObj instance;
    public static MainTowerObj Instance => instance;

    void Awake()
    {
        instance = this;
    }

    public void UpdateHp(int h)
    {
        hp = h;
        PanelManager.Instance.GetPanel<GamePanel>().UpdateMainTowerHP(hp);
    }

    public void Wound(int num)
    {
        if (isDead) return;
        hp -= num;
        if (hp <= 0)
        {
            hp = 0;
            isDead = true;
            PanelManager.Instance.ShowPanel<GameOverPanel>();
        }
        UpdateHp(hp);
    }
}
