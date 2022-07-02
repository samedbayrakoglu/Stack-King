using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : MonoBehaviour
{
    public float rotateSpeed;

    private LevelManager levelManager;
    private CameraController cameraController;


    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();

        cameraController = FindObjectOfType<CameraController>();
    }

    private void Update()
    {
        transform.Rotate(transform.up, rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(EarnCoroutine());
        }
    }

    private IEnumerator EarnCoroutine()
    {
        float distance = Vector3.Distance(transform.position, cameraController.GetCoinCamCoordinates());

        while (distance >= 0.1f)
        {
            distance = Vector3.Distance(transform.position, cameraController.GetCoinCamCoordinates());

            Vector3 direction = cameraController.GetCoinCamCoordinates() - transform.position;

            Vector3 additionVec = direction.normalized * 5f * Time.deltaTime;

            if (additionVec.magnitude >= distance)
                additionVec = direction.normalized * distance;

            transform.position += additionVec;

            Quaternion refRot = transform.rotation;

            transform.Rotate(0, 6.0f * 180f * Time.deltaTime, 0);

            transform.localScale *= 0.97f;

            yield return null;
        }

        levelManager.EarnCoin(1);

        Destroy(gameObject);
    }
}
