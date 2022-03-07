using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIHolder : MonoBehaviour
{
	float tooltipWait = 0.15f;
	private void OnMouseEnter()
	{
		
	}

	IEnumerator ShowTooltip()
	{
		yield return new WaitForSeconds(tooltipWait);

	}
}
