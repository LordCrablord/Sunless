using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Stats {HP, HP_MAX, XP, GOLD}
public class CharacterStats:MonoBehaviour
{
	public string characterName;
	public Sprite sprite;
    [SerializeField] float hpMax;
    public int HpMax
	{
		get
		{
			return Mathf.RoundToInt(
				(hpMax + AddAllBonuses(additiveBonuses, Stats.HP_MAX)) + (hpMax + AddAllBonuses(additiveBonuses, Stats.HP_MAX)) * AddAllBonuses(multiplyingBonuses, Stats.HP_MAX)
				);
		}
		set
		{
			hpMax = value;
		}
	}

	[SerializeField] float hp;
	public float Hp
	{
		get { return hp; }
		set
		{
			if (value > HpMax) hp = HpMax;
			else hp = value;
		}
	}


	protected List<StatModifier> additiveBonuses;
	protected List<StatModifier> multiplyingBonuses;

	protected CharacterStats()
	{
		additiveBonuses = new List<StatModifier>();
		multiplyingBonuses = new List<StatModifier>();
	}

	protected float AddAllBonuses(List<StatModifier> modifiersList, Stats stat)
	{
		return modifiersList.FindAll(st => st.modifierTo == stat).Sum(s => s.value);
	}

	public void AddAdditiveModToList(StatModifier statModifier)
	{
		additiveBonuses.Add(statModifier);
	}

	public void RemoveAdditiveModFromList(StatModifier mod)
	{
		additiveBonuses.Remove(mod);
	}

	public void AddMultiplyingModToList(StatModifier statModifier)
	{
		multiplyingBonuses.Add(statModifier);
	}

	public void RemoveMultiplyingModFromList(StatModifier mod)
	{
		multiplyingBonuses.Remove(mod);
	}
}

[System.Serializable]
public class StatModifier
{
	public int modifierFromID;
	public Stats modifierTo;
	public float value;
}
