using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Character character;

    private Vector3 offset;

    private bool isFollowing = false;

    [SerializeField] float lerpCoef;

    [SerializeField] Transform startTransform;



    private void Awake()
    {
        character = FindObjectOfType<Character>();

        offset = transform.position - character.transform.position;
    }

    public void StartFollow()
    {
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
