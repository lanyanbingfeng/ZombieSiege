
using UnityEngine;

public class RightZombieMove : MonoBehaviour
{
    private static readonly int Dead = Animator.StringToHash("Dead");
    public Animator LeftZombieAnimator;
    public void AtkEvent()
    {
        LeftZombieAnimator.SetTrigger(Dead);
    }
}
