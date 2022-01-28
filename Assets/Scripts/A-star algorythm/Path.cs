using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path
{
    public readonly Vector3[] lookPoints;
    public readonly Line[] turnBoundries;
    public readonly int finishLineIndex;

    public Path(Vector3[] waypoints, Vector3 startPos, float turnDistance)
	{
        lookPoints = waypoints;
        turnBoundries = new Line[lookPoints.Length];
        finishLineIndex = turnBoundries.Length - 1;

        Vector2 previousPoint = V3ToV2(startPos);
        for(int i = 0; i< lookPoints.Length; i++)
		{
            Vector2 currPoint = V3ToV2(lookPoints[i]);
            Vector2 dirrToCurrPoint = (currPoint - previousPoint).normalized;
            Vector2 turnBoundryPoint = (i == finishLineIndex) ? currPoint : currPoint - dirrToCurrPoint * turnDistance;
            turnBoundries[i] = new Line(turnBoundryPoint, previousPoint - dirrToCurrPoint * turnDistance);
            previousPoint = turnBoundryPoint;
        }
	}

    Vector2 V3ToV2(Vector3 v3)
	{
        return new Vector2(v3.x, v3.z);
	}

    public void DrawWithGizmos()
	{
        Gizmos.color = Color.black;
        foreach(Vector3 p in lookPoints)
		{
            Gizmos.DrawCube(p + Vector3.up, Vector3.one);
		}

        Gizmos.color = Color.white;
        foreach(Line l in turnBoundries)
		{
            l.DrawWithGizmos(10);
		}
	}
}
