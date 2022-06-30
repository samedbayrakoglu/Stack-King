using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    private LevelManager levelManager;

    private Button upgradeButton;

    [SerializeField] TextMeshProUGUI priceText;

    private bool isEnabled = true;

    private CanvasGroup canvasGroup;



    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();

        upgradeButton = GetComponent<Button>();
        upgradeButton.onClick.AddListener(OnClick);

        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnClick()
    {
        if (!isEnabled)
            return;

        levelManager.SpendCoin();
    }

    public void UpdatePrice(int currentCoinAmount, int price)
    {
        priceText.text = price.ToString();

        if (price > currentCoinAmount)
        {
            Disable();
        }
        else
        {
            Enable();
        }
    }

    private void Enable()
    {
        isEnabled = true;

        canvasGroup.alpha = 1;
    }

    private void Disable()
    {
        isEnabled = false;

        canvasGroup.alpha = 0.45f;
    }
}
