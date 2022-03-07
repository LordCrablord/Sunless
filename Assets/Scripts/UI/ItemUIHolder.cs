using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIHolder : MonoBehaviour
{
	float tooltipWait = 0.15f;
	[SerializeField] GameObject itemTooltipPrefab;
	GameObject itemTooltip;
	public void OnMouseEnter()
	{
		Debug.Log("aaaaaaaaaaaaaaaa");
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
		itemTooltip.transform.SetParent(gameObject.transform);
	}
}
