using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    private Level level;

    private Collider finishCollider;


    private void Awake()
    {
        finishCollider = GetComponent<Collider>();
    }

    public void Setup(Level _level)
    {
        level = _level;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            level.LevelEnded();
        }
    }
}
