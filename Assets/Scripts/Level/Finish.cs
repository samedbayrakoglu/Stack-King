using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public delegate void OnLevelEnd();
    public OnLevelEnd levelEndEvent;


    private Collider finishCollider;


    private void Awake()
    {
        finishCollider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GameManager.isLevelEnded && other.gameObject.tag == "Player")
        {
            levelEndEvent?.Invoke();
        }
    }
}
