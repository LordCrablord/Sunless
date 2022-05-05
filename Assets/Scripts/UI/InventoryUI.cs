using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
	[SerializeField] GameObject itemPickerScrollContent;
	[SerializeField] GameObject itemUIHolderWidePrefab;
	List<Item> inventoryItems;
	ItemType currentSortType;

	public void SetInventory(List<Item> items)
	{
		inventoryItems = items;
		currentSortType = ItemType.ALL;
		SetItems(currentSortType);
	}

	public void SetItems(ItemType sortType)
	{
		foreach (Item inventoryItem in inventoryItems)
		{
			if(inventoryItem.itemType == sortType || sortType == ItemType.ALL)
			{
				GameObject itemUIHolderWide = Instantiate(itemUIHolderWidePrefab, transform.position, Quaternion.identity);
				itemUIHolderWide.transform.SetParent(itemPickerScrollContent.transform, false);
				itemUIHolderWide.GetComponent<ItemUIHolderWide>().SetUIHolderWide(inventoryItem);
			}
		}
	}
}
