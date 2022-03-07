using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIHolderWide : ItemUIHolder
{
	string inventoryPickerName = "InventoryItemPicker(Clone)";
	protected override IEnumerator ShowItemTooltip()
	{
		yield return new WaitForSeconds(tooltipWait);
		itemTooltip = Instantiate(itemTooltipPrefab, transform.position, Quaternion.identity);
		itemTooltip.transform.SetParent(transform.parent.parent.parent, false);
		itemTooltip.GetComponent<RectTransform>().anchoredPosition = gameObject.GetComponent<RectTransform>().anchoredPosition + itemTooltipOffset;
	}

	public override void OnClick()
	{
		GameObject inventoryPicker = transform.parent.parent.parent.gameObject;
		if (inventoryPicker.name.ToString() == inventoryPickerName)
		{
			inventoryPicker.GetComponent<InventoryItemPicker>(
				).ManagePlayerItemChoice();
		}
		else
		{
			Debug.LogError(inventoryPicker.name.ToString() + " is not " + inventoryPickerName);
			return;
		}
	}
}
