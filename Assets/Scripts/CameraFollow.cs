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
    Vector3 offset;
    Vector3 velocity = Vector3.zero;
    Vector3 cameraVelocity = Vector3.zero;

    private void Start()
	{
        offset = defaultOffset;
	}

	void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
        {
            offset = offset + zoomStep * Input.mouseScrollDelta.y * -1;
            mainCamera.transform.localPosition = offset;
        }
        //mainCamera.transform.position = Vector3.SmoothDamp(mainCamera.transform.position, target.transform.position + offset, ref cameraVelocity, smoothTime);
        /*if (offset.y < minOffset.y || offset.z > minOffset.z)
            offset = minOffset;
        if (offset.y > maxOffset.y || offset.z < maxOffset.z)
            offset = maxOffset;*/

        transform.position = Vector3.SmoothDamp(transform.position,target.transform.position, ref velocity, smoothTime);

       /* if (Input.GetKeyDown(KeyCode.L))
            offset = defaultOffset;*/


    }
}
