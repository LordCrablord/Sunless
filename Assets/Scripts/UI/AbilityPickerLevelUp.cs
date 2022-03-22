using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityPickerLevelUp : AbilityPicker
{
	public override void SetAbilitiesInPicker(PlayerCharacterStats stats)
	{
		playerCharacter = stats;
		List<SpellAbility> spellAbilities = GameManager.Instance.PCAbilityDatabase.abilities.Where(a => !stats.KnownAbilities.Contains(a)).ToList();

		ClearContent();
		foreach (SpellAbility ability in spellAbilities)
		{
			GameObject UIHolderWide = Instantiate(abilityHolderWidePrefab, transform.position, Quaternion.identity);
			UIHolderWide.transform.SetParent(content.transform, false);
			UIHolderWide.GetComponent<AbilityUIHolderCharWide>().SetAbilityHolder(ability, 0);
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
}
