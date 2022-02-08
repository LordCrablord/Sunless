using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform mainCamera;
    public Transform target;

    public Vector3 defaultOffset;
    public Vector3 minOffset;
    public Vector3 maxOffset;
    public Vector3 zoomStep;

    public float smoothTime = 0.3f;
    public float rotationSpeed = 15;

    public float freeFlyCameraSpeed = 3;

    Vector3 offset;
    Vector3 velocity = Vector3.zero;

    public bool lockedOnTarget = true;

    private void Start()
	{
        offset = defaultOffset;
	}

	void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ResetPosition();
            lockedOnTarget = !lockedOnTarget;
        }

        ManageCameraScroll();

        if (lockedOnTarget)
            transform.position = Vector3.SmoothDamp(transform.position, target.transform.position, ref velocity, smoothTime);
        else
            ManageCameraMovement();

        if (Input.GetKey(KeyCode.Mouse2))
        {
            Vector3 rotation = new Vector3(0, rotationSpeed, 0) * Input.GetAxis("Mouse X") * Time.deltaTime;
            transform.Rotate(rotation);
        }
    }

    void ResetPosition()
	{
        offset = defaultOffset;
        mainCamera.transform.localPosition = offset;
        transform.position = target.transform.position;
        transform.rotation = new Quaternion();
    }

    void ManageCameraScroll()
	{
        if (Input.mouseScrollDelta.y != 0)
        {
            offset = offset + zoomStep * Input.mouseScrollDelta.y * -1;
            if (offset.y < minOffset.y || offset.z > minOffset.z)
                offset = minOffset;
            if (offset.y > maxOffset.y || offset.z < maxOffset.z)
                offset = maxOffset;

            mainCamera.transform.localPosition = offset;
        }
    }

    void ManageCameraMovement()
	{
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(freeFlyCameraSpeed * horizontal, 0, freeFlyCameraSpeed * vertical).normalized;
        transform.Translate(direction);
    }
}
