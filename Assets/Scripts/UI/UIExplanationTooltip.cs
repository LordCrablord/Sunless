using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIExplanationTooltip : MonoBehaviour
{
	[SerializeField] GameObject tooltipPrefab;
	[SerializeField] float tooltipTimeWait;
	[SerializeField] Vector2 tooltipOffset;
	GameObject tooltip;
	public void OnMouseEnter()
	{
		StartCoroutine("ShowTooltip");
	}

	public void OnMouseExit()
	{
		StopCoroutine("ShowTooltip");
		if (tooltip != null)
			Destroy(tooltip);
	}

	IEnumerator ShowTooltip()
	{
		yield return new WaitForSeconds(tooltipTimeWait);
		tooltip = Instantiate(tooltipPrefab, transform.position, Quaternion.identity);
		tooltip.transform.SetParent(transform.parent, false);
		tooltip.GetComponent<RectTransform>().anchoredPosition = tooltipOffset + gameObject.GetComponent<RectTransform>().anchoredPosition;
		//tooltip.GetComponent<ItemTooltip>().SetTooltip(item);
	}
}
