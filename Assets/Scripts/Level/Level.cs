using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public Transform charStartPoint;
    public Transform charDancePoint;

    [SerializeField] Finish finishCollider;


    private void Awake()
    {
        Setup();
    }

    private void Setup()
    {
        finishCollider.levelEndEvent += LevelEnded;
    }

    private void LevelEnded()
    {
        finishCollider.levelEndEvent -= LevelEnded;

        Debug.Log("yeahhh level ended");
    }

}
