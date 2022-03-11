using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	const float minPathUpdateTime = 0.2f;
	const float pathUpdateMoveThreshold = 0.5f;

    public Transform target;
    public float defaultSpeed = 10;
	public float turnSpeed = 3;
	public float turnDistance = 5;

	float currentSpeed;

	Path path;

	private void Start()
	{
		GameManager.Instance.GamePaused += OnGamePaused;
		GameManager.Instance.GameResumed += OnGameResumed;
		currentSpeed = defaultSpeed;
		StartCoroutine(UpdatePath());
	}

	public void OnPathFound(Vector3[] waypoints, bool pathSuccsessful)
	{
		if (pathSuccsessful)
		{
			path = new Path(waypoints, transform.position, turnDistance);
			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
	}

	IEnumerator UpdatePath()
	{
		if(Time.timeSinceLevelLoad < .3f)
		{
			yield return new WaitForSeconds(0.3f);
		}
		PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);

		float squereMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
		Vector3 targetPosOld = target.position;

		while (true)
		{
			yield return new WaitForSeconds(minPathUpdateTime);
			if ((target.position - targetPosOld).sqrMagnitude > squereMoveThreshold)
			{
				PathRequestManager.RequestPath(transform.position, target.position, OnPathFound);
				targetPosOld = target.position;
			}
		}
	}

	IEnumerator FollowPath()
	{
		bool followingPath = true;
		int pathIndex = 0;
		if (path.lookPoints.Length != 0)
			transform.LookAt(path.lookPoints[0]);
		else
			followingPath = false;

		while (followingPath)
		{
			Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
			while (path.turnBoundries[pathIndex].HasCrossedLine(pos2D))
			{
				if (pathIndex == path.finishLineIndex)
				{
					followingPath = false;
					break;
				}
				else
					pathIndex++;
			}

			if (followingPath)
			{
				Quaternion targetRotation = Quaternion.LookRotation(path.lookPoints[pathIndex] - transform.position);
				transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
				transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed, Space.Self);
			}

			yield return null;
		}
	}

	public void OnDrawGizmos()
	{
		if(path != null)
		{
			path.DrawWithGizmos();
		}
	}

	void OnGamePaused()
	{
		currentSpeed = 0;
	}
	void OnGameResumed()
	{
		currentSpeed = defaultSpeed;
	}
}
