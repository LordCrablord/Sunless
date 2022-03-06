using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Stats {HP}
public class CharacterStats:MonoBehaviour
{
    float hp;
    public int Hp
	{
		get
		{
			return Mathf.RoundToInt(
				hp+AddAdditiveBonuses(additiveBonuses, Stats.HP)
				
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

	float AddAdditiveBonuses(List<StatModifier> modifiersList, Stats stat)
	{
		return modifiersList.FindAll(st => st.modifierTo == stat).Sum(s => s.value);
	}
}

public class StatModifier
{
	public int modifierFromID;
	public Stats modifierTo;
	public float value;
}
