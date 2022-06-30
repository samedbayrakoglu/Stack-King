using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public delegate void OnLevelStart();
    public OnLevelStart LevelStartEvent;


    [Space]
    public static bool isLevelStarted;
    public static bool isLevelEnded;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        isLevelStarted = false;
        isLevelEnded = false;
    }

    public void LevelStart()
    {
        if (isLevelStarted)
            return;

        isLevelStarted = true;

        LevelStartEvent?.Invoke();
    }

    public void LevelEnded()
    {
        isLevelEnded = true;
    }

    public void LevelLoaded()
    {
        isLevelStarted = false;
        isLevelEnded = false;
    }
}
