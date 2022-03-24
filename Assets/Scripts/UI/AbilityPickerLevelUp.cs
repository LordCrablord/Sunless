using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AbilityPickerLevelUp : AbilityPicker
{
	[SerializeField] Color disabledColor;
	[SerializeField] Color activeColor;
	[SerializeField] Color selectedColor;
	public override void SetAbilitiesInPicker(PlayerCharacterStats stats)
	{
		playerCharacter = stats;
		List<SpellAbility> spellAbilities = GameManager.Instance.PCAbilityDatabase.abilities.Where(a => !stats.KnownAbilities.Contains(a)).ToList();

		ClearContent();
		LevelUpUI levelUpUI = transform.parent.GetComponent<LevelUpUI>();
		foreach (SpellAbility ability in spellAbilities)
		{
			GameObject UIHolderWide = Instantiate(abilityHolderWidePrefab, transform.position, Quaternion.identity);
			UIHolderWide.transform.SetParent(content.transform, false);
			UIHolderWide.GetComponent<AbilityUIHolderCharWide>().SetAbilityHolder(ability, 0);

			if (!levelUpUI.CanTakeAbilityOnLevelUpCheck(ability))
			{
				UIHolderWide.GetComponent<Button>().interactable = false;
				UIHolderWide.GetComponent<Image>().color = disabledColor;
			}
			else
			{
				UIHolderWide.GetComponent<Image>().color = activeColor;
			}
		}

	}

	public void ClearContent()
	{
		for (int i = 0; i < content.transform.childCount; i++)
		{
			Destroy(content.transform.GetChild(i).gameObject);
		}
	}

	public void ManageLevelUpAbilityChoise(GameObject originator, Ability ability)
	{
		transform.parent.GetComponent<LevelUpUI>().ToggleAbilityPick(originator, ability);
	}

	public void SetActiveColorForAbility(GameObject originator, bool isSelected)
	{
		if (isSelected)
			originator.GetComponent<Image>().color = selectedColor;
		else
			originator.GetComponent<Image>().color = activeColor;
	}
}
