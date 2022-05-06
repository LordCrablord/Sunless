using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
	[SerializeField] GameObject itemScrollContent;
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
		foreach (Transform child in itemScrollContent.transform)
		{
			Destroy(child.gameObject);
		}

		foreach (Item inventoryItem in inventoryItems)
		{
			if(inventoryItem.itemType == sortType || sortType == ItemType.ALL ||
				(sortType == ItemType.CHESTPIECE && inventoryItem.itemType == ItemType.HELMET))
			{
				GameObject itemUIHolderWide = Instantiate(itemUIHolderWidePrefab, transform.position, Quaternion.identity);
				itemUIHolderWide.transform.SetParent(itemScrollContent.transform, false);
				itemUIHolderWide.GetComponent<ItemUIHolderWide>().SetUIHolderWide(inventoryItem);
			}
		}
	}

	public void OnSortClick(int itemTypeIntValue)
	{
		ItemType type = (ItemType)itemTypeIntValue;
		SetItems(type);
	}
}
