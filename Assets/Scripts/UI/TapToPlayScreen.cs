using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


public class TapToPlayScreen : Screen
{
    public UpgradeButton upgradeButton;

    [SerializeField] CanvasGroup fadePanel;

    [SerializeField] TextMeshProUGUI levelNo;

    [SerializeField] TextMeshProUGUI coinAmount;



    public override void Show()
    {
        base.Show();

        fadePanel.DOFade(0f, 1f).From(1f);
    }

    public override void Hide()
    {
        base.Hide();
    }

    public void UpdateLevelNo(int no)
    {
        levelNo.text = "level: " + no.ToString();
    }

    public void UpdateCoinAmount(int amount)
    {
        coinAmount.text = amount.ToString();
    }


}
