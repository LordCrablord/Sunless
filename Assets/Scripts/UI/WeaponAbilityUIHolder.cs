using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponAbilityUIHolder : MonoBehaviour
{
    WeaponAttackAbility attackAbility;
    [SerializeField] Image image;
    
    public void SetWeaponAbilityUI(WeaponAttackAbility ab)
	{
        attackAbility = ab;
        image.sprite = attackAbility.sprite;
	}
}
