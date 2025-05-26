using UnityEngine;
using UnityEngine.Events;

public class CameraAnimator : MonoBehaviour
{
    private Animator animator;
    private UnityAction turnOverEvent;
    void Start()
    {
        //显示初始面板
        PanelManager.Instance.ShowPanel<BeginPanel>();
        //得到动画脚本
        animator = GetComponent<Animator>();        
    }

    public void TurnLeftOrRight(bool isLeft,UnityAction callback)
    {
        //提供给外部使用  摄像机当前是左转还是右转的动画状体
        animator.SetBool(GameDataMgr.LeftOrRight,isLeft);
        turnOverEvent += callback;
    }
    //动画事件调用的方法，当动画播放结束时调用
    private void TurnOver()
    {
        turnOverEvent?.Invoke();
        turnOverEvent = null;
    }
}
