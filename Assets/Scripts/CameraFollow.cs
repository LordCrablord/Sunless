using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 defaultOffset;
    public Vector3 minOffset;
    public Vector3 zoomStep;
    public float smoothTime = 0.3f;
    Vector3 offset;
    Vector3 velocity = Vector3.zero;

	private void Start()
	{
        offset = defaultOffset;
	}

	void Update()
    {
        if (Input.mouseScrollDelta.y != 0)
            offset = offset + zoomStep * Input.mouseScrollDelta.y * -1;
        transform.position = Vector3.SmoothDamp(transform.position,target.transform.position + offset, ref velocity, smoothTime);
    }
}
