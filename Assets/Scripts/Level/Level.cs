using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private LevelManager levelManager;

    public Transform charStartPoint;
    public Transform charDancePoint;

    [SerializeField] Finish finishCollider;


    private void Awake()
    {
        Setup();

        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Setup()
    {
        finishCollider.Setup(this);
    }

    public void LevelEnded()
    {
        levelManager.LevelEnded();
    }

}
