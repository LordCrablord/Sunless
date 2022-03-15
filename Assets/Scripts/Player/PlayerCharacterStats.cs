using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New PlayerCharacter", menuName = "Character/PlayerCharacter")]
public class PlayerCharacterStats : CharacterStats
{
    static int xp;
    public float Xp
	{
		get
		{
			return xp;
		}
		set
		{
			xp = Mathf.RoundToInt(
				(value + AddAllBonuses(additiveBonuses, Stats.XP)) + (value + AddAllBonuses(additiveBonuses, Stats.XP)) * AddAllBonuses(multiplyingBonuses, Stats.XP)
				);
			while (xp >= levelXpThreshold[level])
			{
				level++;
				Debug.Log(characterName + " is now level " + level);
			}
		}
	}

	public readonly Dictionary<int, int> levelXpThreshold = new Dictionary<int, int>()
	{
		{0, 1},
		{ 1, 100},
		{ 2, 200},
		{ 3, 500},
		{ 4, 800},
	};

	int level;
	public int Level
	{
		get { return level; }
	}

	static int gold;
	public float Gold
	{
		get
		{
			return gold;
		}
		set
		{
			gold = Mathf.RoundToInt(
				(value + AddAllBonuses(additiveBonuses, Stats.GOLD)) + (value + AddAllBonuses(additiveBonuses, Stats.GOLD)) * AddAllBonuses(multiplyingBonuses, Stats.GOLD)
				);
			Debug.Log("Gold: " + gold);
		}
	}

	[SerializeField] int protPierceBase;
	public float ProtPierce
	{
		get{return ReturnProtArmorClass(protPierceBase, Stats.PROT_PIERCE);}
		set { protPierceBase = (int)value; }
	}

	[SerializeField] int protSlashBase;
	public float ProtSlash
	{
		get { return ReturnProtArmorClass(protSlashBase, Stats.PROT_SLASH); }
		set { protSlashBase = (int)value; }
	}

	[SerializeField] int protBludgeBase;
	public float ProtBludge
	{
		get { return ReturnProtArmorClass(protBludgeBase, Stats.PROT_BLUDGE); }
		set { protBludgeBase = (int)value; }
	}

	[SerializeField] int protElementalBase;
	public float ProtElement
	{
		get { return ReturnProtArmorClass(protElementalBase, Stats.PROT_ELEMENT); }
		set { protElementalBase = (int)value; }
	}

	[SerializeField] int protEldrichBase;
	public float ProtEldrich
	{
		get { return ReturnProtArmorClass(protEldrichBase, Stats.PROT_ELDRICH); }
		set { protEldrichBase = (int)value; }
	}

	[SerializeField] int protArcaneBase;
	public float ProtArcane
	{
		get { return ReturnProtArmorClass(protArcaneBase, Stats.PROT_ARCANE); }
		set { protArcaneBase = (int)value; }
	}

	int ReturnProtArmorClass(float baseStat, Stats protStat)
	{
		float res = baseStat + AddAllBonuses(additiveBonuses, protStat);
		res = res + res * AddAllBonuses(multiplyingBonuses, protStat);
		if (helmet != null) res += helmet.GetArmorProtVal(protStat);
		if (chestpiece != null) res += chestpiece.GetArmorProtVal(protStat);
		return (int)res;
	}

	public float Damage
	{
		get
		{
			float weaponValue = weapon != null ? weapon.damage : 0;
			float addBonusesSum = weaponValue + AddAllBonuses(additiveBonuses, Stats.DAMAGE);
			float multBonusesSum = AddAllBonuses(multiplyingBonuses, Stats.DAMAGE);
			return Mathf.RoundToInt(addBonusesSum + addBonusesSum * multBonusesSum);
		}
	}

	public float CritChance
	{
		get
		{
			float weaponStat = weapon != null ? weapon.critChance : 0;
			float addBonusesSum = weaponStat + AddAllBonuses(additiveBonuses, Stats.CRIT_CHANCE);
			float multBonusesSum = AddAllBonuses(multiplyingBonuses, Stats.CRIT_CHANCE);
			return Mathf.RoundToInt(addBonusesSum + addBonusesSum * multBonusesSum);
		}
	}

	public float CritValue
	{
		get
		{
			float weaponStat = weapon != null ? weapon.critValue : 0;
			float addBonusesSum = weaponStat + AddAllBonuses(additiveBonuses, Stats.CRIT_VALUE);
			float multBonusesSum = AddAllBonuses(multiplyingBonuses, Stats.CRIT_VALUE);
			return addBonusesSum + addBonusesSum * multBonusesSum;
		}
	}
	//TODO maybe make one func for those things above so it would take less space

	//public Dictionary<Stats, Func<object>> StatsDictionary;
	

	static List<Item> inventoryBack = new List<Item>();
	public List<Item> InventoryBack { get { return inventoryBack; } }

	public Armor helmet;
	public Armor chestpiece;
	public Weapon weapon;
	public Item trinket1;

	public PlayerCharacterStats()
	{
		StatsDictionary.Add(Stats.XP, new VariableReference(() => Xp, val => { Xp = (float)val; }));
		StatsDictionary.Add(Stats.GOLD, new VariableReference(() => Gold, val => { Gold = (float)val; }));
		StatsDictionary.Add(Stats.DAMAGE, new VariableReference(() => Damage, null));
		StatsDictionary.Add(Stats.CRIT_CHANCE, new VariableReference(() => CritChance, null));
		StatsDictionary.Add(Stats.CRIT_VALUE, new VariableReference(() => CritValue, null));
		
		StatsDictionary.Add(Stats.PROT_PIERCE, new VariableReference(() => ProtPierce, val => { ProtPierce = (float)val; }));
		StatsDictionary.Add(Stats.PROT_SLASH, new VariableReference(() => ProtSlash, val => { ProtSlash = (float)val; }));
		StatsDictionary.Add(Stats.PROT_BLUDGE, new VariableReference(() => ProtBludge, val => { ProtBludge = (float)val; }));
		StatsDictionary.Add(Stats.PROT_ELEMENT, new VariableReference(() => ProtElement, val => { ProtElement = (float)val; }));
		StatsDictionary.Add(Stats.PROT_ELDRICH, new VariableReference(() => ProtEldrich, val => { ProtEldrich = (float)val; }));
		StatsDictionary.Add(Stats.PROT_ARCANE, new VariableReference(() => ProtArcane, val => { ProtArcane = (float)val; }));
	}

	public void EquipItem(Item item)
	{
		if (item == null) return;

		switch (item)
		{
			case Weapon w: weapon = w;	break;
			case Armor a:
				if (a.itemType == ItemType.HELMET)	helmet = a;
				else if (a.itemType == ItemType.CHESTPIECE)	chestpiece = a;
				break;
			case Item t: trinket1 = t; break;
			default:
				Debug.LogError("Unable to find class type for " + item.name);
				return;
		}
		AddBonuses(item);
		inventoryBack.Remove(item);
	}

	void AddBonuses(Item item)
	{
		foreach(StatModifier sm in item.additiveBonuses)
			AddAdditiveModToList(sm);
		foreach (StatModifier sm in item.multiplyingBonuses)
			AddMultiplyingModToList(sm);
	}

	public void UnequipItem(Item item)
	{
		if (item == null) return;

		switch (item)
		{
			case Weapon w: weapon = null; break;
			case Armor a:
				if (a.itemType == ItemType.HELMET) helmet = null;
				else if (a.itemType == ItemType.CHESTPIECE) chestpiece = null;
				break;
			case Item t: trinket1 = null; break;
			default:
				Debug.LogError("Unable to find class type for " + item.name);
				return;
		}
		RemoveBonuses(item);
		inventoryBack.Add(item);
	}

	void RemoveBonuses(Item item)
	{
		foreach (StatModifier sm in item.additiveBonuses)
			RemoveAdditiveModFromList(sm);
		foreach (StatModifier sm in item.multiplyingBonuses)
			RemoveMultiplyingModFromList(sm);
	}

}

sealed public class VariableReference
{
	public Func<object> Get { get; private set; }
	public Action<object> Set { get; private set; }
	public VariableReference(Func<object> getter, Action<object> setter)
	{
		Get = getter;
		Set = setter;
	}
}
