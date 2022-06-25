using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private CharacterController controller;

    [SerializeField] float movementSpeed = 5f;





    private void Awake()
    {
        SetupComponents();
    }

    private void Update()
    {

    }

    private void SetupComponents()
    {
        controller = GetComponent<CharacterController>();
    }
}
