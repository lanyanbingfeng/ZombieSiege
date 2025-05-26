using UnityEngine;

public class PlayerObj : MonoBehaviour
{
    private static readonly int HSpeed = Animator.StringToHash("HSpeed");
    private static readonly int VSpeed = Animator.StringToHash("VSpeed");
    private static readonly int Atk = Animator.StringToHash("Atk");
    private static readonly int Reload = Animator.StringToHash("Reload");
    private static readonly int Roll = Animator.StringToHash("Roll");

    private float moveSpeed = 3;
    private float rotateSpeed = 100;

    public int Hp = 100;
    public int atk = 10;
    
    private Animator animator;
    private CharacterController player;
    
    private bool isRun;
    private bool isSquat;
    private float SquatWeight;
    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GetComponent<CharacterController>();
    }

    void Update()
    {
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
}
