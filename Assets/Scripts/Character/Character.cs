using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Character : MonoBehaviour
{
    private GameManager gameManager;
    private LevelManager levelManager;

    #region  Control
    private CharacterController controller;

    [SerializeField] Animator animator;
    private float runBlendValue = 0f;

    public float verticalMovementSpeed = 8f;
    public float horizontalMovementSpeed = 4f;

    private Vector3 moveVec; // movement vector

    private Vector2 delta;
    private Vector2 smoothDelta;
    private Vector2 refVel = Vector2.zero;

    private float lastPosX = 0;
    #endregion

    [Space]
    public StackBar stackBar;


    private void Awake()
    {
        SetupComponents();

        stackBar.Show();
    }

    private void Update()
    {
        if (GameManager.isLevelStarted && !GameManager.isLevelEnded)
        {
            Move();
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

    private void LevelStart()
    {
        gameManager.LevelStartEvent -= LevelStart;

        if (animator != null)
        {
            animator.SetBool("Run", true);

            stackBar.Show();
        }
        else
        {
            Debug.Log("Animator is missing!!!");
        }
    }

    private void SetupComponents()
    {
        gameManager = FindObjectOfType<GameManager>(); // get it for once
        gameManager.LevelStartEvent += LevelStart;

        levelManager = FindObjectOfType<LevelManager>();

        controller = GetComponent<CharacterController>();
    }

    public void GoToDancePoint(Transform target)
    {
        transform.DOLookAt(target.position, 0.2f); // rotate char to dance point

        transform.DOMove(target.position, 1f).SetEase(Ease.Linear).OnComplete(() => //move char to dance point
        {
            transform.DOLookAt(Vector3.zero, 0.2f).OnComplete(() =>
            {
                animator.SetBool("Dance", true);

                stackBar.Hide();

                Camera.main.GetComponent<CameraController>().StopFollow();

                DOVirtual.DelayedCall(1f, () => // delayed call for end screen
                {
                    FindObjectOfType<UIManager>().levelEndScreen.Show();
                });

            });
        });
    }

    public void SetFillAmount(float amount)
    {
        stackBar.SetFillAmount(amount);

        DOTween.To(x => runBlendValue = x, runBlendValue, amount, 0.5f).OnUpdate(() =>
        {
            animator.SetFloat("Blend", runBlendValue);
        });
    }

    public void DirectlySetFillBar(float amount)
    {
        stackBar.DirectlySetFillBar(amount);

        animator.SetFloat("Blend", amount);
    }
}
