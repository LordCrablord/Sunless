using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmorWeight { LIGHT, MEDIUM, HEAVY }

[CreateAssetMenu(fileName = "New Armor", menuName = "Items/Armor")]
public class Armor : Item
{
    public ArmorWeight armorWeight;
    public int armorValue;
}
