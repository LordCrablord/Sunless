using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemUIHolderWide : ItemUIHolder
{
	string inventoryPickerName = "InventoryItemPicker(Clone)";
	[SerializeField] TextMeshProUGUI nameTMP;
	protected override IEnumerator ShowItemTooltip()
	{
		yield return new WaitForSeconds(tooltipWait);
		itemTooltip = Instantiate(itemTooltipPrefab, transform.position, Quaternion.identity);
		itemTooltip.transform.SetParent(transform.parent.parent.parent, false);
		//TODO: it is wrong in case of fucking scroll
		itemTooltip.GetComponent<RectTransform>().anchoredPosition = gameObject.GetComponent<RectTransform>().anchoredPosition + itemTooltipOffset;
		itemTooltip.GetComponent<ItemTooltip>().SetTooltip(item);
	}

	public override void OnClick()
	{
		GameObject inventoryPicker = transform.parent.parent.parent.gameObject;
		if (inventoryPicker.name.ToString() == inventoryPickerName)
		{
			inventoryPicker.GetComponent<InventoryItemPicker>(
				).ManagePlayerItemChoice(item);
		}
		else
		{
			Debug.LogError(inventoryPicker.name.ToString() + " is not " + inventoryPickerName);
			return;
		}
	}

	public void SetUIHolderWide(Item newItem)
	{
		SetUIHolder(newItem);
		itemType = newItem.itemType;
		nameTMP.text = newItem.itemName;
	}
}
