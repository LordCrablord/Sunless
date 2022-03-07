using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIHolder : MonoBehaviour
{
	float tooltipWait = 0.25f;
	[SerializeField] GameObject itemTooltipPrefab;
	[SerializeField] Vector2 itemTooltipOffset = new Vector2(70, -150);
	GameObject itemTooltip;
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

	IEnumerator ShowItemTooltip()
	{
		Debug.Log("Dfafadfasdfadsfasdfasdfasdf");
		yield return new WaitForSeconds(tooltipWait);
		itemTooltip = Instantiate(itemTooltipPrefab, transform.position, Quaternion.identity);
		itemTooltip.transform.SetParent(transform.parent, false);
		itemTooltip.GetComponent<RectTransform>().anchoredPosition = itemTooltipOffset + gameObject.GetComponent<RectTransform>().anchoredPosition;
	}
}
