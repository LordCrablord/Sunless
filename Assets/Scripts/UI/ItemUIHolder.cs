using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIHolder : MonoBehaviour
{
	protected float tooltipWait = 0.25f;
	[SerializeField] protected GameObject itemTooltipPrefab;
	[SerializeField] protected Vector2 itemTooltipOffset = new Vector2(70, -150);
	protected GameObject itemTooltip;
	public void OnMouseEnter()
	{
		StartCoroutine("ShowItemTooltip");
	}

	public void OnMouseExit()
	{
		StopCoroutine("ShowItemTooltip");
		if (itemTooltip != null) 
			Destroy(itemTooltip);
	}

	protected virtual IEnumerator ShowItemTooltip()
	{
		yield return new WaitForSeconds(tooltipWait);
		itemTooltip = Instantiate(itemTooltipPrefab, transform.position, Quaternion.identity);
		itemTooltip.transform.SetParent(transform.parent, false);
		itemTooltip.GetComponent<RectTransform>().anchoredPosition = itemTooltipOffset + gameObject.GetComponent<RectTransform>().anchoredPosition;
	}
}
