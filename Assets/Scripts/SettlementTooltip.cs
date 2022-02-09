using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementTooltip : MonoBehaviour
{
	public float tooltipWait = 0.5f;
	public GameObject tooltipPrefab;
	public string tooltipTitle;
	public string tooltipBody;

	GameObject tooltip;
	private void OnMouseEnter()
	{
		StartCoroutine("ShowTooltip");
	}

	private void OnMouseExit()
	{
		StopCoroutine("ShowTooltip");
		if(tooltip!=null)
			Destroy(tooltip);
	}

	IEnumerator ShowTooltip()
	{
		yield return new WaitForSeconds(tooltipWait);

		Vector3 objectPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		tooltip = Instantiate(tooltipPrefab, transform.position, Quaternion.identity);
		tooltip.GetComponent<Tooltip>().SetTooltipText(tooltipTitle, tooltipBody);
	}
}
