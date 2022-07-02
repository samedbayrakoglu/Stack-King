using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;


public class LevelEndScreen : Screen
{
    private LevelManager levelManager;

    [Header("Fade Objects")]
    public Image blackBG;
    public TextMeshProUGUI levelTxt;
    public Image coinIcon;
    public CanvasGroup coinAmountDisplay;
    public CanvasGroup nextButton;

    [Space]
    public TextMeshProUGUI coinAmount;



    public override void Awake()
    {
        blackBG.DOFade(0f, 0f);
        coinIcon.DOFade(0f, 0f);
        levelTxt.alpha = 0;
        coinAmountDisplay.alpha = 0;
        nextButton.alpha = 0;

        base.Awake();
    }

    public override void Show()
    {
        BeforeShow();

        canvasGroup.alpha = 1;

        float fadeTime = 0.3f;

        blackBG.DOFade(0.7f, fadeTime).OnComplete(() =>
        {
            levelTxt.DOFade(1f, fadeTime).SetDelay(0.5f).OnComplete(() =>
            {
                coinIcon.DOFade(1f, fadeTime).SetDelay(0.5f).OnComplete(() =>
                {
                    coinAmountDisplay.DOFade(1f, fadeTime).SetDelay(0.2f).OnComplete(() =>
                    {
                        nextButton.DOFade(1f, fadeTime).SetDelay(0.5f);
                    });
                });
            });
        });
    }

    public override void Hide()
    {
        base.Hide();

        blackBG.DOFade(0f, 0f);
        coinIcon.DOFade(0f, 0f);
        levelTxt.alpha = 0;
        coinAmountDisplay.alpha = 0;
        nextButton.alpha = 0;
    }

    public void NextButtonClicked()
    {
        if (levelManager == null)
            levelManager = FindObjectOfType<LevelManager>();

        levelManager.LoadNextLevel();
    }

    public void UpdateCoinAmount(int amount)
    {
        coinAmount.text = amount.ToString();
    }
}
