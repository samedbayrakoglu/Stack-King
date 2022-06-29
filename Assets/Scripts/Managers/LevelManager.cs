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
    private CameraController cameraController;

    [SerializeField] GameObject characterPrefab;
    private Character character;

    [Space]
    [SerializeField] List<LevelUnit> levelUnits;

    private Dictionary<int, LevelUnit> levelDict = new();

    private int currentLevelNo;
    private Level currentLevel;

    public int coinAmount = 0;

    public int UpgradeAmount
    {
        get
        {
            upgradeAmount *= 2;

            return upgradeAmount;
        }
    }
    private int upgradeAmount = 10;




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
        GameManager.Instance.LevelStartEvent += LevelStarted;

        GameManager.Instance.LevelLoaded();

        cameraController.MoveToStart();

        SaveData();
    }

    private void LoadNextLevel()
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
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("currentLevel", currentLevelNo);
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
    }

    private LevelUnit GetLevel(int no)
    {
        return levelDict[no];
    }


    public void SpendCoin()
    {
        coinAmount -= upgradeAmount;

        Debug.Log(upgradeAmount);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LoadNextLevel();
        }
    }
}
