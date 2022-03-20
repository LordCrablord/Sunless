using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityPicker : MonoBehaviour
{

	public readonly Vector2 inventoryOffset = new Vector2(-248, 0);
	[SerializeField] protected GameObject content;
	[SerializeField] protected GameObject abilityHolderWidePrefab;

	protected PlayerCharacterStats playerCharacter;
	protected Ability oldAbility;
	protected int oldPos;

	public virtual void SetAbilitiesInPicker(PlayerCharacterStats stats)
	{
		playerCharacter = stats;
		List<SpellAbility> spellAbilities = stats.KnownAbilities.Where(a=>!stats.ActiveAbilities.Contains(a)).ToList();
		foreach(SpellAbility ability in spellAbilities)
		{
			GameObject UIHolderWide = Instantiate(abilityHolderWidePrefab, transform.position, Quaternion.identity);
			UIHolderWide.transform.SetParent(content.transform, false);
			UIHolderWide.GetComponent<AbilityUIHolderCharWide>().SetAbilityHolder(ability, 0);
		}
	
	}

	public virtual void ManageChoise(Ability ability)
	{
		if (ability == null) OnCancel();
		
		playerCharacter.ActiveAbilities[oldPos] = (SpellAbility)ability;

		GameManager.Instance.SetCharacterDataOnUI(playerCharacter);
		OnCancel();

	}

	public void OnUnequip()
	{
		playerCharacter.ActiveAbilities[oldPos] = null;

		GameManager.Instance.SetCharacterDataOnUI(playerCharacter);
		OnCancel();
	}

	public void OnCancel()
	{
		Destroy(gameObject);
	}

	internal void SetOldAbility(AbilityUIHolderCharDefault data)
	{
		oldAbility = data.GetAbility();
		oldPos = data.GetAbilityPosition();
	}
}
