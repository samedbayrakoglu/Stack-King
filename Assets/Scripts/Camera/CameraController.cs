using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform character;

    private Vector3 offset;

    private bool isFollowing = false;

    [SerializeField] float lerpCoef;

    [SerializeField] Transform startTransform;



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
