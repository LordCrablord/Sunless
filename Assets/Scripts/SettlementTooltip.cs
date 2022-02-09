using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementTooltip : MonoBehaviour
{
	public float tooltipWait = 0.5f;

	private void OnMouseEnter()
	{
		StartCoroutine("ShowTooltip");
	}

	private void OnMouseExit()
	{
		StopCoroutine("ShowTooltip");
	}

	IEnumerator ShowTooltip()
	{
		yield return new WaitForSeconds(tooltipWait);
		print(gameObject.name);
	}
}
