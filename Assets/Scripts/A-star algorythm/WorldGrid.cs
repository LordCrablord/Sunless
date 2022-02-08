﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGrid : MonoBehaviour
{

	public bool displayGridGizmos;
	public LayerMask unwalkableMask;
	public Vector2 gridWorldSize;
	public float nodeRadius;
	public TerrainType[] walkableRegions;
	public int obstacleProximityPenalty;
	LayerMask walkableMask;
	Dictionary<int, int> walkableRegionsDictionary = new Dictionary<int, int>();

	Node[,] grid;

	float nodeDiameter;
	int gridSizeX, gridSizeY;

	int penaltyMin = int.MaxValue;
	int penaltyMax = int.MinValue;

	private void Awake()
	{
		nodeDiameter = nodeRadius * 2;
		gridSizeX = Mathf.RoundToInt(gridWorldSize.x / nodeDiameter);
		gridSizeY = Mathf.RoundToInt(gridWorldSize.y / nodeDiameter);

		foreach (TerrainType region in walkableRegions)
		{
			walkableMask.value += region.terrainMask.value;
			walkableRegionsDictionary.Add((int)Mathf.Log(region.terrainMask.value, 2), region.terrainPenalty);
		}

		CreateGrid();
	}

	public int MaxSize
	{
		get
		{
			return gridSizeX * gridSizeY;
		}
	}
	public Node GetNodeFromWorldPoint(Vector3 worldPosition)
	{
		float percentX = (worldPosition.x + gridWorldSize.x / 2) / gridWorldSize.x;
		float percentY = (worldPosition.z + gridWorldSize.y / 2) / gridWorldSize.y;
		percentX = Mathf.Clamp01(percentX);//to avoid errors if our char is out of the grid for some reason
		percentY = Mathf.Clamp01(percentY);

		int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
		int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

		return grid[x, y];
	}

	void CreateGrid()
	{
		grid = new Node[gridSizeX, gridSizeY];
		Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.forward * gridWorldSize.y / 2;

		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = 0; y < gridSizeY; y++)
			{
				Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * nodeDiameter + nodeRadius) + Vector3.forward * (y * nodeDiameter + nodeRadius);
				bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));

				int movementPenalty = 0;
				
				Ray ray = new Ray(worldPoint + Vector3.up * 50, Vector3.down);
				RaycastHit hit;
				if(Physics.Raycast(ray, out hit, 100, walkableMask))
				{
					walkableRegionsDictionary.TryGetValue(hit.collider.gameObject.layer, out movementPenalty);
				}

				if (!walkable)
				{
					movementPenalty += obstacleProximityPenalty;
				}

				grid[x, y] = new Node(walkable, worldPoint, x, y, movementPenalty);
			}
		}

		BlurPenaltyMap(1);
	}

	void BlurPenaltyMap(int blurSize)
	{
		int kernelSize = blurSize * 2 + 1;


		int[,] penaltiesHorizontalPass = new int[gridSizeX, gridSizeY];
		int[,] penaltiesVerticalPass = new int[gridSizeX, gridSizeY];

		for(int y = 0; y<gridSizeY; y++)
		{
			for(int x = -blurSize; x <= blurSize; x++)
			{
				int sampleX = Mathf.Clamp(x, 0, blurSize);
				penaltiesHorizontalPass[0, y] += grid[sampleX, y].movementPenalty;
			}
			for(int x = 1; x < gridSizeX; x++)
			{
				int removeIndex = Mathf.Clamp(x - blurSize - 1, 0, gridSizeX);
				int addIndex = Mathf.Clamp(x + blurSize, 0, gridSizeX-1);

				penaltiesHorizontalPass[x, y] = penaltiesHorizontalPass[x - 1, y] - grid[removeIndex, y].movementPenalty + grid[addIndex, y].movementPenalty;
			}
		}

		for (int x = 0; x < gridSizeX; x++)
		{
			for (int y = -blurSize; y <= blurSize; y++)
			{
				int sampleY = Mathf.Clamp(y, 0, blurSize);
				penaltiesVerticalPass[x, 0] += penaltiesHorizontalPass[x, sampleY];
			}
			int blurredPenalty = Mathf.RoundToInt((float)penaltiesVerticalPass[x, 0] / (kernelSize * kernelSize));
			grid[x, 0].movementPenalty = blurredPenalty;

			for (int y = 1; y < gridSizeY; y++)
			{
				int removeIndex = Mathf.Clamp(y - blurSize - 1, 0, gridSizeY);
				int addIndex = Mathf.Clamp(y + blurSize, 0, gridSizeY - 1);
				penaltiesVerticalPass[x, y] = penaltiesVerticalPass[x, y - 1] -	penaltiesHorizontalPass[x, removeIndex] + penaltiesHorizontalPass[x, addIndex];
				blurredPenalty = Mathf.RoundToInt((float)penaltiesVerticalPass[x, y] / (kernelSize * kernelSize));
				grid[x, y].movementPenalty = blurredPenalty;

				if (blurredPenalty > penaltyMax)
					penaltyMax = blurredPenalty;
				if (blurredPenalty < penaltyMin)
					penaltyMin = blurredPenalty;
			}
		}
	}

	public List<Node> GetNodeNeighbors(Node node)
	{
		List<Node> neighbors = new List<Node>();

		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				if (x == 0 && y == 0)
					continue;

				int checkX = node.gridX + x;
				int checkY = node.gridY + y;

				if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
				{
					neighbors.Add(grid[checkX, checkY]);
				}
			}
		}

		return neighbors;
	}
	//Pathfinding debugging
	//public List<Node> path;
	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, 1, gridWorldSize.y));
				
		if (grid != null && displayGridGizmos)
		{
			foreach (Node n in grid)
			{

				Gizmos.color = Color.Lerp(Color.white, Color.black, Mathf.InverseLerp(penaltyMin, penaltyMax, n.movementPenalty));

				/*if (path != null)
					if (path.Contains(n))
						Gizmos.color = Color.green;
				*/

				Gizmos.color = (n.walkable) ? Gizmos.color : Color.red;
				Gizmos.DrawCube(n.worldPosition, Vector3.one * (nodeDiameter));
			}
		}
		
	}

	[System.Serializable]
	public class TerrainType
	{
		public LayerMask terrainMask;
		public int terrainPenalty;
	}
}