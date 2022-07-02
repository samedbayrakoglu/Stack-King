using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum StartLocation
{
    Left,
    Right
}
public class Obstacle : MonoBehaviour
{
    public StartLocation startLocation = new();

    public float rotateSpeed;

    private LevelManager levelManager;



    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();

        SetMovementX();
    }

    private void SetMovementX()
    {
        if (startLocation == StartLocation.Left)
        {
            transform.position = new Vector3(-2, transform.position.y, transform.position.z);

            transform.DOMoveX(transform.position.x + 4, 3f).SetDelay(Random.Range(0.4f, 0.6f)).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
        else if (startLocation == StartLocation.Right)
        {
            transform.position = new Vector3(2, transform.position.y, transform.position.z);

            transform.DOMoveX(transform.position.x - 4, 3f).SetDelay(Random.Range(0, 0.2f)).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, 1), rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            levelManager.Unstack(2);

            Camera.main.transform.DOShakePosition(0.05f, 0.1f, 10, 90);
        }
    }

    private void OnDestroy()
    {
        DOTween.Kill(transform, false);
    }
}
