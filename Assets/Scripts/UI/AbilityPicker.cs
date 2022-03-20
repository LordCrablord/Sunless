using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPicker : MonoBehaviour
{

	public readonly Vector2 inventoryOffset = new Vector2(-248, 0);
	string parentUICanvasName = "CharacterCanvasUI";

	public void SetAbilitiesInPicker(List<SpellAbility> abilities)
	{

	}

	public void OnCancel()
	{
		Destroy(gameObject);
	}
}
