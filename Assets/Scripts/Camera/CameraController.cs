using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform character;

    private UIManager uIManager;

    private Vector3 offset;

    private bool isFollowing = false;

    [SerializeField] float lerpCoef;

    [SerializeField] Transform startTransform;




    private void Awake()
    {
        uIManager = FindObjectOfType<UIManager>();
    }

    public void StartFollow(Transform target)
    {
        character = target;

        offset = transform.position - target.position;

        isFollowing = true;
    }

    public void StopFollow()
    {
        isFollowing = false;
    }

    private void Update()
    {
        if (isFollowing)
        {
            Follow();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            Vector2 dasda = uIManager.inGameScreen.GetCoinScreenPoint();

            Debug.Log(dasda);
        }
    }

    public Vector3 GetCoinCamCoordinates()
    {
        Vector2 coinScreenCoordinate = uIManager.inGameScreen.GetCoinScreenPoint();

        Camera cam = GetComponent<Camera>();

        Vector3 coinUIWorldPoint = cam.ViewportToWorldPoint(new Vector3(coinScreenCoordinate.x, coinScreenCoordinate.y, cam.nearClipPlane + 3));

        return coinUIWorldPoint;
    }

    public void MoveToStart()
    {
        transform.position = startTransform.localPosition;
        transform.rotation = startTransform.localRotation;
    }

    private void Follow()
    {
        Vector3 desiredPosition = new Vector3(Mathf.Clamp(character.transform.position.x, -0.5f, 0.5f), transform.position.y, character.transform.position.z + offset.z);

        Vector3 lerpedPosition = Vector3.Lerp(transform.position, desiredPosition, lerpCoef * Time.deltaTime);// for smooth lerp

        transform.position = lerpedPosition;
    }


}
