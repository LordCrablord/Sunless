using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType { HELMET, CHESTPIECE, WEAPON, TRINKET}

[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName ="Items/Item")]
public class Item : ScriptableObject
{
    public int id;
    public ItemType itemType;
    public string itemName;
    public string description;
    public Sprite sprite;
    public int goldCost;

    public List<StatModifier> additiveBonuses;
    public List<StatModifier> multiplyingBonuses;
}
