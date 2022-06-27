using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void OnGameStart();
    public OnGameStart GameStartEvent;

    public delegate void OnGameOver();
    OnGameOver GameOverEvent;

    [Space]
    public static bool isLevelEnded;
    public static bool isGameStarted;





    private void Awake()
    {
        isLevelEnded = false;
        isGameStarted = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isGameStarted)
        {
            isGameStarted = true;

            GameStartEvent?.Invoke();
        }
    }
}
