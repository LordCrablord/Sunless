using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmorType {HELMET, CHEST_PIECE}
public enum ArmorWeight { LIGHT, MEDIUM, HEAVY }

[CreateAssetMenu(fileName = "New Armor", menuName = "Items/Armor")]
public class Armor : Item
{
    public ArmorType armorType;
    public ArmorWeight armorWeight;
    public int armorValue;
}
