using UnityEngine;

public class PlayerObj : MonoBehaviour
{
    private static readonly int HSpeed = Animator.StringToHash("HSpeed");
    private static readonly int VSpeed = Animator.StringToHash("VSpeed");
    private static readonly int Atk = Animator.StringToHash("Atk");
    private static readonly int Reload = Animator.StringToHash("Reload");
    private static readonly int Roll = Animator.StringToHash("Roll");
    
    private static PlayerObj instance;
    public static PlayerObj Instance => instance;

    void Awake()
    {
        instance = this;
    }

    private float moveSpeed = 3;
    private float rotateSpeed = 100;

    public int hp = 100;
    public int atk;
    public int atkType;
    public int haveGameGold = 200;
    
    private Animator animator;
    private CharacterController player;
    
    private bool isRun;
    private bool isSquat;
    private float SquatWeight;
    private bool isDead;

    public Transform atkPos;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<CharacterController>();
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        foreach (Transform children in allChildren)
        {
            if (children.name == "AtkPos") atkPos = children;
        }
    }

    public void InitAttribute(HeroData data)
    {
        atk = data.atk;
        atkType = data.atkType;
        //初始金币200
        UpdateGameGold(haveGameGold);
    }

    public void Wound(int damage)
    {
        if (isDead) return;
        hp -= damage;
        if (hp <= 0)
        {
            hp = 0;
            Dead();
        }
        animator.SetTrigger("Wound");
        GameDataMgr.Instance.PlaySound("Wound");
        PanelManager.Instance.GetPanel<GamePanel>().UpdatePlayerHP(hp);
    }

    public void Dead()
    {
        isDead = true;
        animator.SetTrigger("Dead");
        GameDataMgr.Instance.PlaySound("Dead");
    }

    public void DeadEvent()
    {
        PanelManager.Instance.ShowPanel<GameOverPanel>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        
        if (isDead || MainTowerObj.Instance.isDead) return;
        if (Input.GetKeyDown(KeyCode.LeftShift)) isRun = true;
        else if (Input.GetKeyUp(KeyCode.LeftShift)) isRun = false;
        
        if (Input.GetKeyDown(KeyCode.C)) isSquat = true;
        else if (Input.GetKeyUp(KeyCode.C)) isSquat = false;

        if (Input.GetMouseButtonDown(0)) animator.SetBool(Atk, true);
        
        if (Input.GetKeyDown(KeyCode.R)) animator.SetTrigger(Reload);
        
        if (Input.GetKeyUp(KeyCode.Space)) animator.SetTrigger(Roll);
        
        animator.SetFloat(HSpeed,Input.GetAxis("Horizontal") * (isRun ? 1 : 0.5f));
        animator.SetFloat(VSpeed, Input.GetAxis("Vertical") * (isRun ? 1 : 0.5f));

        if (isSquat) SquatWeight = Mathf.Lerp(SquatWeight,1,Time.deltaTime * 10);
        else SquatWeight = Mathf.Lerp(SquatWeight,0,Time.deltaTime * 10);
        animator.SetLayerWeight(animator.GetLayerIndex("Squat"),SquatWeight);
        
        Vector3 newdir = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        player.Move(newdir * moveSpeed * Time.deltaTime * (isRun ? 1 : 0.5f));
        
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime * Input.GetAxis("Mouse X"));
    }
    
    //当动画播放到攻击对面的时候调用
    public void AtkEvent()
    {
        //近战攻击
        if (atkType == 1)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position + transform.forward + transform.up,1,1 << LayerMask.NameToLayer("Monster"));
            for (int i = 0; i < colliders.Length; i++)
            {
                colliders[i].GetComponent<ZombieObj>().Wound(atk);
                break;
            }
        }
        //远程攻击
        else if (atkType == 2)
        {
            GameDataMgr.Instance.PlaySound("Gun");
            if (Physics.Raycast(new Ray(atkPos.position, transform.forward), out RaycastHit hit,1000,1 << LayerMask.NameToLayer("Monster")))
            {
                hit.collider.GetComponent<ZombieObj>().Wound(atk);
            }
        }
    }

    public void UpdateGameGold(int money)
    {
        PanelManager.Instance.GetPanel<GamePanel>().UpdateGold(money);
    }

    public void AddGold(int money)
    {
        haveGameGold += money;
        UpdateGameGold(haveGameGold);
    }
}
