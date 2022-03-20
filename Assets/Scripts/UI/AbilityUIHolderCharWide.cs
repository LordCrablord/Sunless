using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityUIHolderCharWide : AbilityUIHolderChar
{
	[SerializeField] TextMeshProUGUI nameTMP;
	public override void SetAbilityHolder(Ability a, int pos)
	{
		base.SetAbilityHolder(a, pos);
		nameTMP.text = ability.abilityName;
	}

	protected override IEnumerator ShowAbilityInfo()
	{
		yield return new WaitForSeconds(tooltipWait);
		abilityInfo = Instantiate(AbilityInfoPrefab, transform.position, Quaternion.identity);
		abilityInfo.transform.SetParent(transform.parent.parent.parent.parent, false);
		abilityInfo.GetComponent<RectTransform>().anchoredPosition = itemTooltipOffset + gameObject.GetComponent<RectTransform>().anchoredPosition;
		abilityInfo.GetComponent<AbilityInfo>().SetAbilityTooltip(ability);
	}

	public void OnClick()
	{
		transform.parent.parent.parent.gameObject.GetComponent<AbilityPicker>().ManageChoise(ability);
	}
}
