using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Stats {HP}
public class CharacterStats:MonoBehaviour
{
    [SerializeField] float hp;
    public int Hp
	{
		get
		{
			return Mathf.RoundToInt(
				(hp + AddAllBonuses(additiveBonuses, Stats.HP)) + (hp + AddAllBonuses(additiveBonuses, Stats.HP)) * AddAllBonuses(multiplyingBonuses, Stats.HP)
				);
		}
		set
		{
			hp = value;
		}
	}

	List<StatModifier> additiveBonuses;
	List<StatModifier> multiplyingBonuses;

	CharacterStats()
	{
		additiveBonuses = new List<StatModifier>();
		multiplyingBonuses = new List<StatModifier>();
	}

	float AddAllBonuses(List<StatModifier> modifiersList, Stats stat)
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

public class StatModifier
{
	public int modifierFromID;
	public Stats modifierTo;
	public float value;
}
