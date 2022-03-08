﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { SWORD, MACE}
public enum handUsage { ONE_HANDED, TWO_HANDED}
public enum damageType { SLASHING, BLUDGEONING, FIRE}

[CreateAssetMenu(fileName = "New Weapon", menuName = "Items/Weapon")]
public class Weapon : Item
{
    public WeaponType weaponType;
    public handUsage handUsage;
    public int damage;
    public float critChance;
    public float critValue;
}