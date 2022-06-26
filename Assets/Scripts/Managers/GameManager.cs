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
    public static bool isGameOver;
    public static bool isGameStarted;





    private void Awake()
    {
        isGameOver = false;
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
