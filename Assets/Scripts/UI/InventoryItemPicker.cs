using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryItemPicker : MonoBehaviour
{
	public readonly Vector2 inventoryOffset = new Vector2(-48,0);
	[SerializeField] GameObject itemPickerScrollContent;
	[SerializeField] GameObject itemUIHolderWidePrefab;

	public void SetItems(ItemType type, List<Item> items)
	{
		List<Item> requiredItems = items.Where(i => i.itemType==type).ToList();
		
		foreach(Item inventoryItem in requiredItems)
		{
			GameObject itemUIHolderWide = Instantiate(itemUIHolderWidePrefab, transform.position, Quaternion.identity);
			itemUIHolderWide.transform.SetParent(itemPickerScrollContent.transform, false);
			itemUIHolderWide.GetComponent<ItemUIHolderWide>().SetUIHolderWide(inventoryItem);
		}
	}

	public void ManagePlayerItemChoice()
	{
		//TODO later something
		Destroy(gameObject);
	}
}
