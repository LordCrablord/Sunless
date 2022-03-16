using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityUIHolder : WeaponAbilityUIHolder
{
	[SerializeField] TextMeshProUGUI cooldownTMP;
	public void SetAbilityUI(Ability ab, int position)
	{
		ability = ab;
		if(ability != null)
		{
			image.sprite = ability.sprite;
			SetImageTransparency(1);
			SetUIHolder(position);
			if (BattleManager.Instance.CurrentCharacter.abilityCooldowns.ContainsKey((SpellAbility)ability))
			{
				GetComponent<Button>().interactable = false;
				panelBlocker.SetActive(true);
				cooldownTMP.gameObject.SetActive(true);
				cooldownTMP.text = BattleManager.Instance.CurrentCharacter.abilityCooldowns[(SpellAbility)ability].ToString();
			}
			else
			{
				cooldownTMP.gameObject.SetActive(false);
			}
		}
		else
		{
			image.sprite = null;
			SetImageTransparency(0);
			GetComponent<Button>().interactable = true;
			panelBlocker.SetActive(false);
			cooldownTMP.gameObject.SetActive(false);
		}
	}

	void SetImageTransparency(float transparency)
	{
		Color color = image.color;
		color.a = transparency;
		image.color = color;
	}
}
