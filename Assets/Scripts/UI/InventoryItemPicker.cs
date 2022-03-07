using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemPicker : MonoBehaviour
{
	public readonly Vector2 inventoryOffset = new Vector2(-48,0);

	public void SetPositionToObject(GameObject itemHolder)
	{

	}

	public void ManagePlayerItemChoice()
	{
		gameObject.SetActive(false);
	}
}
