using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelUnit
{
    public int levelID;
    public GameObject levelPrefab;
}

public class LevelManager : MonoBehaviour
{
    private UIManager uIManager;

    private CameraController cameraController;

    [SerializeField] GameObject characterPrefab;
    private Character character;

    [Space]
    [SerializeField] List<LevelUnit> levelUnits;

    private Dictionary<int, LevelUnit> levelDict = new();

    private int currentLevelNo;
    private Level currentLevel;

    public int coinAmount = 0;

    private int upgradeDegree = 0;
    private int upgradePrice;




    private void Awake()
    {
        SetComponents();

        SetLevels();

        LoadData();

        LoadLevel(currentLevelNo);

        SpawnCharacter();

        SaveData();
    }

    private void SpawnCharacter()
    {
        GameObject charObj = Instantiate(characterPrefab, currentLevel.charStartPoint.position, Quaternion.identity);

        character = charObj.GetComponent<Character>();
    }

    private void LoadLevel(int no)
    {
        LevelUnit unit = GetLevel(no);

        GameObject levelObj = Instantiate(unit.levelPrefab);
        currentLevel = levelObj.GetComponent<Level>();

        AfterLevelLoad();
    }

    private void AfterLevelLoad()
    {
        uIManager.UpdateLevelNo(currentLevelNo);

        uIManager.UpdateCoinAmount(coinAmount);

        CalculateUpgradePrice();
        uIManager.tapToPlayScreen.upgradeButton.UpdatePrice(coinAmount, upgradePrice);

        GameManager.Instance.LevelStartEvent += LevelStarted;

        GameManager.Instance.LevelLoaded();

        cameraController.MoveToStart();

        SaveData();

        uIManager.tapToPlayScreen.Show();
    }

    public void LoadNextLevel()
    {
        DestroyCurrentLevel();

        currentLevelNo += 1;

        if (currentLevelNo > levelUnits.Count)
        {
            currentLevelNo = 1;
        }

        LoadLevel(currentLevelNo);

        SpawnCharacter();
    }

    public void LevelEnded()
    {
        GameManager.Instance.LevelEnded();

        character.GoToDancePoint(currentLevel.charDancePoint);
    }

    private void LevelStarted()
    {
        cameraController.StartFollow(character.transform);

        uIManager.inGameScreen.Show();
    }

    private void DestroyCurrentLevel()
    {
        Destroy(character.gameObject);

        Destroy(currentLevel.gameObject);
    }

    private void LoadData()
    {
        currentLevelNo = PlayerPrefs.GetInt("currentLevel");
        if (currentLevelNo < 1)
        {
            currentLevelNo = 1;
        }

        upgradeDegree = PlayerPrefs.GetInt("upgradeDegree");

        coinAmount = PlayerPrefs.GetInt("coinAmount");
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("currentLevel", currentLevelNo);
        PlayerPrefs.SetInt("upgradeDegree", upgradeDegree);
        PlayerPrefs.SetInt("coinAmount", coinAmount);
    }

    private void SetLevels()
    {
        foreach (LevelUnit unit in levelUnits)
        {
            levelDict.Add(unit.levelID, unit);
        }
    }

    private void SetComponents()
    {
        cameraController = FindObjectOfType<CameraController>();

        uIManager = FindObjectOfType<UIManager>();
    }

    private LevelUnit GetLevel(int no)
    {
        return levelDict[no];
    }


    public void EarnCoin(int amount)
    {
        coinAmount += amount;

        uIManager.UpdateCoinAmount(coinAmount);

        SaveData();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            EarnCoin(1);
        }


    }

    public void SpendCoin()
    {
        coinAmount -= upgradePrice;

        upgradeDegree += 1;
        CalculateUpgradePrice();

        uIManager.UpdateCoinAmount(coinAmount);

        uIManager.tapToPlayScreen.upgradeButton.UpdatePrice(coinAmount, upgradePrice);

        SaveData();
    }

    private void CalculateUpgradePrice()
    {
        upgradePrice = (int)Mathf.Pow(2, upgradeDegree);
    }
}
