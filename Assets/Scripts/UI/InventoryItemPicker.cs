using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryItemPicker : MonoBehaviour
{
	public readonly Vector2 inventoryOffset = new Vector2(-48,0);

	public void SetItems(ItemType type, List<Item> items)
	{
		List<Item> requiredItem = items.Where(i => i.itemType==type).ToList();
		Debug.Log(requiredItem.Count);
	}

	public void ManagePlayerItemChoice()
	{
		//TODO later something
		Destroy(gameObject);
	}
}
