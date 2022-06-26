using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private GameManager gameManager;

    #region  Control
    private CharacterController controller;

    private Animator animator;

    public float verticalMovementSpeed = 8f;
    public float horizontalMovementSpeed = 4f;

    private Vector3 moveVec; // movement vector

    private Vector2 delta;
    private Vector2 smoothDelta;
    private Vector2 refVel = Vector2.zero;

    private float lastPosX = 0;
    #endregion

    [Space]
    [SerializeField] StackBar stackBar;


    private void Awake()
    {
        SetupComponents();
    }

    private void Update()
    {
        if (GameManager.isGameStarted)
        {
            Move();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stackBar.SetFillAmount(0.2f);
        }
    }

    private void Move()
    {
        controller.Move(transform.forward * Time.deltaTime * verticalMovementSpeed); // forward movement

        if (Input.GetMouseButtonDown(0))
        {
            lastPosX = Input.mousePosition.x; // delta will be zero at first touch
        }

        if (Input.GetMouseButton(0))
        {
            delta.x = Input.mousePosition.x - lastPosX; // delta last frame

            smoothDelta = Vector2.Lerp(smoothDelta, delta, 100f * Time.deltaTime); // get smooth delta

            moveVec = Vector3.Lerp(moveVec, smoothDelta.x * 0.2f * Vector3.right, Time.deltaTime * 11f); // lerp movement vector

            if (moveVec != Vector3.zero)
            {
                controller.Move(moveVec * Time.deltaTime * horizontalMovementSpeed);
            }

            lastPosX = Input.mousePosition.x;
        }
    }

    private void GameStart()
    {
        if (animator != null)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            Debug.Log("Animator is missing!!!");
        }

        lastPosX = Input.mousePosition.x;
    }

    private void SetupComponents()
    {
        gameManager = FindObjectOfType<GameManager>(); // get it for once
        gameManager.GameStartEvent += GameStart;

        controller = GetComponent<CharacterController>();

        animator = GetComponentInChildren<Animator>();
    }


}
