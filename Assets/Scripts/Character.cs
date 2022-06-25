using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private CharacterController controller;

    public float verticalMovementSpeed = 8f;
    public float horizontalMovementSpeed = 4f;
    public float rotationSpeed = 12f;

    private Vector3 moveVec; // movement vector
    private Vector3 verticalVelocity;

    private bool canMove;

    private Vector2 delta;
    private Vector2 smoothDelta;
    private Vector2 refVel = Vector2.zero;

    private float lastPosX = 0;



    private void Awake()
    {
        SetupComponents();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastPosX = Input.mousePosition.x;
        }

        if (Input.GetMouseButton(0))
        {
            controller.Move(transform.forward * Time.deltaTime * verticalMovementSpeed); // forward movement

            delta.x = Input.mousePosition.x - lastPosX; // delta last frame

            smoothDelta = Vector2.Lerp(smoothDelta, delta, 0.3f); // get smooth delta

            moveVec = Vector3.Lerp(moveVec, smoothDelta.x * 0.2f * Vector3.right, Time.deltaTime * 11f); // lerp movement vector

            if (moveVec != Vector3.zero)
            {
                controller.Move(moveVec * Time.deltaTime * horizontalMovementSpeed);
            }

            lastPosX = Input.mousePosition.x;
        }
    }

    private void SetupComponents()
    {
        controller = GetComponent<CharacterController>();
    }
}
