using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityUIHolderCharWide : AbilityUIHolderChar
{
	[SerializeField] TextMeshProUGUI nameTMP;
	public override void SetAbilityHolder(Ability a)
	{
		base.SetAbilityHolder(a);
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
}
