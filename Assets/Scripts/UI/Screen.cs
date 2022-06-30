using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Screen : MonoBehaviour
{
    private UIManager uIManager;

    protected CanvasGroup canvasGroup;


    public virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        gameObject.SetActive(false);
    }

    public virtual void Show()
    {
        BeforeShow();

        canvasGroup.DOFade(1f, 0.6f).From(0f);
    }

    public virtual void Hide()
    {
        CheckUIManager();

        canvasGroup.DOFade(0f, 0.15f).From(1f);
    }

    protected void BeforeShow()
    {
        CheckUIManager();

        if (uIManager.currentScreen != null)
        {
            uIManager.currentScreen.Hide();

            uIManager.currentScreen.gameObject.SetActive(false);
        }

        uIManager.currentScreen = this;

        gameObject.SetActive(true);
    }

    public void CheckUIManager()
    {
        if (uIManager == null)
        {
            uIManager = FindObjectOfType<UIManager>();
        }
    }
}
