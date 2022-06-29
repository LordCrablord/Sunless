using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityUIHolderCharWide : AbilityUIHolderChar
{
	[SerializeField] TextMeshProUGUI nameTMP;
	[SerializeField] TextMeshProUGUI levelUpConditionTMP;
	public override void SetAbilityHolder(Ability a, int pos)
	{
		base.SetAbilityHolder(a, pos);
		nameTMP.text = ability.abilityName;
		levelUpConditionTMP.text = ((SpellAbility)a).levelUpConditionsString;
	}

	protected override IEnumerator ShowAbilityInfo()
	{
		yield return new WaitForSeconds(tooltipWait);
		abilityInfo = Instantiate(AbilityInfoPrefab, transform.position, Quaternion.identity);
		abilityInfo.transform.SetParent(transform.parent.parent.parent, false);
		abilityInfo.GetComponent<RectTransform>().anchoredPosition = itemTooltipOffset + gameObject.GetComponent<RectTransform>().anchoredPosition;
		if(abilityInfo.GetComponent<RectTransform>().anchoredPosition.y < 200)
        {
			abilityInfo.GetComponent<RectTransform>().anchoredPosition = new Vector2(-420, 0);
		}
		abilityInfo.GetComponent<AbilityInfo>().SetAbilityTooltip(ability);
	}

	public void OnClick()
	{
		transform.parent.parent.parent.gameObject.GetComponent<AbilityPicker>().ManageChoise(ability);
	}

	public void OnLevelUpAbilityClick()
	{
		transform.parent.parent.parent.gameObject.GetComponent<AbilityPickerLevelUp>().ManageLevelUpAbilityChoise(gameObject, ability);
	}
}
