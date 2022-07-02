using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Gem : MonoBehaviour
{
    public GameObject hitEffect;

    public float rotateSpeed;

    private LevelManager levelManager;



    private void Awake()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void Update()
    {
        transform.Rotate(transform.up, rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SpawnHitEffect();

            levelManager.Stack(1);

            Destroy(gameObject);
        }
    }

    private void SpawnHitEffect()
    {
        Instantiate(hitEffect, transform.position, transform.rotation);
    }
}
