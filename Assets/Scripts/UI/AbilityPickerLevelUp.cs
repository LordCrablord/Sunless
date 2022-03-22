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
		foreach (SpellAbility ability in spellAbilities)
		{
			GameObject UIHolderWide = Instantiate(abilityHolderWidePrefab, transform.position, Quaternion.identity);
			UIHolderWide.transform.SetParent(content.transform, false);
			UIHolderWide.GetComponent<AbilityUIHolderCharWide>().SetAbilityHolder(ability, 0);
		}

	}

	public void ManageLevelUpAbilityChoise(GameObject originator, Ability ability)
	{
		Debug.Log("It works");
	}
}
