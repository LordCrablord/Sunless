using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIHolder : MonoBehaviour
{
	float tooltipWait = 0.25f;
	[SerializeField] GameObject itemTooltipPrefab;
	[SerializeField] Vector3 itemTooltipOffset = new Vector3(70, -150, 0);
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
		itemTooltip.transform.SetParent(gameObject.transform, false);
		itemTooltip.GetComponent<RectTransform>().anchoredPosition = itemTooltipOffset;
	}
}
