﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { SWORD, MACE}
public enum HandUsage { ONE_HANDED, TWO_HANDED}
public enum damageType { SLASHING, BLUDGEONING, FIRE}

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]
public class Weapon : Item
{
    public WeaponType weaponType;
    public HandUsage handUsage;
    public int minDamage;
    public int maxDamage;
    public float critChance;
    public float critValue;
    public Stats DamageDestination;
    public WeaponAttackAbility mainAbility;
    public WeaponAttackAbility secondAbility;
}
