using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUIHolder : WeaponAbilityUIHolder
{
	public void SetAbilityUI(Ability ab, int position)
	{
		ability = ab;
		image.sprite = ability.sprite;
		SetUIHolder(position);
	}
}
