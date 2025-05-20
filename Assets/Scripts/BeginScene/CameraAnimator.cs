using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CameraAnimator : MonoBehaviour
{
    private Animator animator;
    private UnityAction turnOverEvent;
    void Start()
    {
        PanelManager.Instance.ShowPanel<BeginPanel>();
        animator = GetComponent<Animator>();        
    }

    public void TurnLeftOrRight(bool isLeft,UnityAction callback)
    {
        animator.SetBool(GameDataMgr.LeftOrRight,isLeft);
        turnOverEvent += callback;
    }

    private void TurnOver()
    {
        turnOverEvent?.Invoke();
        turnOverEvent = null;
    }
}
