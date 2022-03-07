using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIHolderWide : ItemUIHolder
{
	protected override IEnumerator ShowItemTooltip()
	{
		yield return new WaitForSeconds(tooltipWait);
		itemTooltip = Instantiate(itemTooltipPrefab, transform.position, Quaternion.identity);
		itemTooltip.transform.SetParent(transform.parent.parent.parent, false);
		itemTooltip.GetComponent<RectTransform>().anchoredPosition = gameObject.GetComponent<RectTransform>().anchoredPosition + itemTooltipOffset;
	}
}
