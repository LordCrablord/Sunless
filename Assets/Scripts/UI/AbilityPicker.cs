using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityPicker : MonoBehaviour
{

	public readonly Vector2 inventoryOffset = new Vector2(-248, 0);
	string parentUICanvasName = "CharacterCanvasUI";
	[SerializeField] GameObject content;
	[SerializeField] GameObject abilityHolderWidePrefab;

	public void SetAbilitiesInPicker(PlayerCharacterStats stats)
	{
		List<SpellAbility> spellAbilities = stats.KnownAbilities.Where(a=>!stats.ActiveAbilities.Contains(a)).ToList();
		foreach(SpellAbility ability in spellAbilities)
		{
			GameObject UIHolderWide = Instantiate(abilityHolderWidePrefab, transform.position, Quaternion.identity);
			UIHolderWide.transform.SetParent(content.transform, false);
			UIHolderWide.GetComponent<AbilityUIHolderCharWide>().SetAbilityHolder(ability);
		}
	
	}

	public void OnCancel()
	{
		Destroy(gameObject);
	}
}
