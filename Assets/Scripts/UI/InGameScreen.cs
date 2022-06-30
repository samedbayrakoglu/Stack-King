using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InGameScreen : Screen
{
    [SerializeField] TextMeshProUGUI coinAmount;
    [SerializeField] TextMeshProUGUI levelNo;



    public override void Show()
    {
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }

    public void UpdateCoinAmount(int amount)
    {
        coinAmount.text = amount.ToString();
    }

    public void UpdateLevelNo(int no)
    {
        levelNo.text = "level: " + no.ToString();
    }
}
