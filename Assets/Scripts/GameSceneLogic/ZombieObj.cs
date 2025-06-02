
using UnityEngine;
using UnityEngine.AI;

public class ZombieObj : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animator;
    private float time;
    
    public ZombieData zombieData; //当前僵尸数据
    public int hp;
    public int atk;
    public float atkoffect; //攻击间隔
    public bool isDead;

    private bool isMove;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        InitAttribute(zombieData);
    }

    void Update()
    {
        if (!isMove || isDead) return;
        float playerDistance = Vector3.Distance(transform.position , PlayerObj.Instance.transform.position);
        float mainTowerDistance = Vector3.Distance(transform.position , MainTowerObj.Instance.transform.position);
        agent.SetDestination(playerDistance < mainTowerDistance ? PlayerObj.Instance.transform.position : MainTowerObj.Instance.transform.position);
        animator.SetBool("Run",agent.velocity != Vector3.zero);
        if (playerDistance < 3 || mainTowerDistance < 5)
        {
            time += Time.deltaTime;
            if (time >= atkoffect)
            {
                animator.SetTrigger("Atk");
                time = 0;
            }
        }
        else time = 0;
    }

    public void InitAttribute(ZombieData zombieData)
    {
        hp = zombieData.hp;
        atk = zombieData.atk;
        atkoffect = zombieData.atkoffect;
        agent.speed = agent.acceleration = zombieData.moveSpeed;
        agent.angularSpeed = zombieData.rotateSpeed;
    }

    public void Wound(int damage)
    {
        if (isDead) return;
        animator.SetTrigger("Wound");
        hp -= damage;
        GameDataMgr.Instance.PlaySound("Wound");
        if (hp <= 0) Dead();
    }

    public void AtkEvent()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward + transform.up, 1);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                //玩家受伤
                PlayerObj.Instance.Wound(atk);
            }
            else if (colliders[i].CompareTag("MainTower"))
            {
                MainTowerObj.Instance.Wound(atk);
            }
        }
    }
    //动画事件 出生之后
    public void BornOver()
    {
        isMove = true;
    }

    public void Dead()
    {
        isDead = true;
        animator.SetBool("Dead", isDead);
        //播放死亡音效
        GameDataMgr.Instance.PlaySound("Dead");
        agent.isStopped = true;
        
        //游戏金币增加
        PlayerObj.Instance.AddGold(10);
    }

    public void DeadEvent()
    {
        Destroy(gameObject);
    }
}
