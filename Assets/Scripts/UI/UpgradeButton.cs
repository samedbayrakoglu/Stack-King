using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    private LevelManager levelManager;

    private Button upgradeButton;

    [SerializeField] TextMeshProUGUI price;

    private bool isEnabled = true;



    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();

        upgradeButton = GetComponent<Button>();
        upgradeButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        levelManager.SpendCoin();

        price.text = levelManager.UpgradeAmount.ToString();
    }

    private void Enable()
    {

    }

    private void Disable()
    {

    }
}
