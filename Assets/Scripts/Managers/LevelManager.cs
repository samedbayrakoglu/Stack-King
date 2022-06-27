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
    [SerializeField] GameObject characterPrefab;
    private Character character;

    [Space]
    [SerializeField] List<LevelUnit> levelUnits;

    private Dictionary<int, LevelUnit> levelDict = new();

    private int currentLevelNo;
    private Level currentLevel;



    private void Awake()
    {
        SetupLevels();

        LoadData();

        LoadLevel(currentLevelNo);

        SpawnCharacter();
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
    }

    private void LevelEnded()
    {

    }

    private void LoadData()
    {
        int levelNo = PlayerPrefs.GetInt("currentLevel");

        if (levelNo < 1)
        {
            currentLevelNo = 1;
        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt("currentLevel", currentLevelNo);
    }

    private void SetupLevels()
    {
        foreach (LevelUnit unit in levelUnits)
        {
            levelDict.Add(unit.levelID, unit);
        }
    }

    private LevelUnit GetLevel(int no)
    {
        return levelDict[no];
    }
}
