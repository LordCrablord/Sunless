using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 defaultOffset;
    public float smoothTime = 0.3f;
    Vector3 offset;
    Vector3 velocity = Vector3.zero;

	private void Start()
	{
        offset = defaultOffset;
	}

	void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position,target.transform.position + offset, ref velocity, smoothTime);
    }
}
