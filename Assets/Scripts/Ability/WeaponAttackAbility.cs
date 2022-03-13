using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New WeaponAttackAbility", menuName = "Ability/WeaponAttackAbility")]
public class WeaponAttackAbility : Ability
{
    public float baseDmgValueAddMod;
    public float baseDmgValueMultMod;
    public float baseCrtChanceAddMod;
    public float baseCrtChanceMultMod;
    public float baseCrtValueAddMod;
    public float baseCrtValueMultMod;

}
