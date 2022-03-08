using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryItemPicker : MonoBehaviour
{
	public readonly Vector2 inventoryOffset = new Vector2(-48,0);
	[SerializeField] GameObject itemPickerScrollContent;
	[SerializeField] GameObject itemUIHolderWidePrefab;
	string parentUICanvasName = "CharacterCanvasUI";
	ItemUIHolder itemUIHolder;
	public void SetItems(ItemUIHolder itemHolder, List<Item> items)
	{
		itemUIHolder = itemHolder;
		List<Item> requiredItems = items.Where(i => i.itemType== itemUIHolder.itemType).ToList();
		
		foreach(Item inventoryItem in requiredItems)
		{
			GameObject itemUIHolderWide = Instantiate(itemUIHolderWidePrefab, transform.position, Quaternion.identity);
			itemUIHolderWide.transform.SetParent(itemPickerScrollContent.transform, false);
			itemUIHolderWide.GetComponent<ItemUIHolderWide>().SetUIHolderWide(inventoryItem);
		}
	}

	public void ManagePlayerItemChoice(Item pickedItem)
	{
		if(transform.parent.gameObject.name != parentUICanvasName)
		{
			Debug.LogError("Error in item picker, " + transform.parent.gameObject.name + " is not" + parentUICanvasName);
			return;
		}
		transform.parent.gameObject.GetComponent<CharacterUI>().SwapItems(itemUIHolder.GetItem(), pickedItem);
		Destroy(gameObject);
	}

	public void OnCancelClick()
	{
		Destroy(gameObject);
	}

	public void OnUnequipClick()
	{
		ManagePlayerItemChoice(null);
	}

}
