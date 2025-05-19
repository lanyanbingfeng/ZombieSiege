using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour 
{
    private CanvasGroup canvasGroup; // 控制 面板 总体透明度
    private float alphaSpeed = 10; // 渐变速度
    private bool isShowing;
    private UnityAction hidePanelcall;
    
    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    protected virtual void Start()
    {
        PanelInit();
    } 
    
    public abstract void PanelInit(); // 面板初始化

    public virtual void Show()
    {
        canvasGroup.alpha = 0;
        isShowing = true;
    }

    public virtual void Hide(UnityAction hidePanelCallback = null)
    {
        canvasGroup.alpha = 1;
        isShowing = false;
        hidePanelcall = hidePanelCallback;
    }

    void Update()
    {
        if (isShowing && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha >= 1) canvasGroup.alpha = 1;
        }

        if (!isShowing && canvasGroup.alpha != 0)
        {
            canvasGroup.alpha -= alphaSpeed * Time.deltaTime;
            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                hidePanelcall?.Invoke();
            }
        }
    }
}
