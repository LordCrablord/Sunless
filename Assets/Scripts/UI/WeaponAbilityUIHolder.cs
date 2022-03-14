using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponAbilityUIHolder : MonoBehaviour
{
    WeaponAttackAbility attackAbility;
    [SerializeField] Image image;
    [SerializeField] GameObject AbilityInfoPrefab;
    GameObject abilityInfo;
	[SerializeField] protected Vector2 itemTooltipOffset = new Vector2(-800, 0);
	float tooltipWait = 0.25f;

	public void SetWeaponAbilityUI(WeaponAttackAbility ab)
	{
        attackAbility = ab;
        image.sprite = attackAbility.sprite;
	}

	public void OnAbilityCliked()
	{
		if(attackAbility != null)
		{
			BattleManager.Instance.selectedAbility = attackAbility;
			Debug.Log(BattleManager.Instance.selectedAbility.abilityName + " is selected");
		}
	}

	public void OnMouseEnter()
	{
		if (attackAbility != null)
			StartCoroutine("ShowAbilityInfo");
	}

	public void OnMouseExit()
	{
		StopCoroutine("ShowAbilityInfo");
		if (abilityInfo != null)
			Destroy(abilityInfo);
	}

	protected virtual IEnumerator ShowAbilityInfo()
	{
		yield return new WaitForSeconds(tooltipWait);
		abilityInfo = Instantiate(AbilityInfoPrefab, transform.position, Quaternion.identity);
		abilityInfo.transform.SetParent(transform.parent.parent, false);
		abilityInfo.GetComponent<RectTransform>().anchoredPosition = itemTooltipOffset + gameObject.GetComponent<RectTransform>().anchoredPosition;
		abilityInfo.GetComponent<AbilityInfo>().SetAbilityTooltip(attackAbility);
	}
}
