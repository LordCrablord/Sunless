using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Stats {HP, HP_MAX, XP, GOLD, DAMAGE, CRIT_CHANCE, CRIT_VALUE}
[System.Serializable]
[CreateAssetMenu(fileName = "New Character", menuName = "Character/Character")]
public class CharacterStats:ScriptableObject
{
	public string characterName;
	public Sprite sprite;
    [SerializeField] float hpMax;
    public float HpMax
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
			OnMaxHealthChanged();
		}
	}

	public event Notify MaxHealthChanged;
	protected virtual void OnMaxHealthChanged()
	{
		MaxHealthChanged?.Invoke();
	}

	[SerializeField] float hp;
	public float Hp
	{
		get { return hp; }
		set
		{
			if (value > HpMax) hp = HpMax;
			else hp = value;
			OnHealthChanged();
		}
	}

	public event Notify HealthChanged;
	protected virtual void OnHealthChanged()
	{
		HealthChanged?.Invoke();
	}

	[SerializeField] float initiative;
	public float Initiative
	{
		get { return initiative; }
		set{ initiative = value;}
	}

	protected List<StatModifier> additiveBonuses;
	protected List<StatModifier> multiplyingBonuses;

	public Dictionary<Stats, VariableReference> StatsDictionary = new Dictionary<Stats, VariableReference>();
	protected CharacterStats()
	{
		additiveBonuses = new List<StatModifier>();
		multiplyingBonuses = new List<StatModifier>();

		StatsDictionary.Add(Stats.HP, new VariableReference(() => Hp, val => { Hp = (float)val; }));
		StatsDictionary.Add(Stats.HP_MAX, new VariableReference(() => HpMax, val => { HpMax = (float)val; }));
	}

	/*protected CharacterStats(CharacterStats character)
	{
		this.characterName = character.characterName;
		this.sprite = character.sprite;
		this.hpMax = character.hpMax;
		this.hp = character.hp;
		this.initiative = character.initiative;

		this.additiveBonuses = new List<StatModifier>();
		foreach (StatModifier modifier in character.additiveBonuses)
			this.additiveBonuses.Add(modifier);

		this.multiplyingBonuses = new List<StatModifier>();
		foreach (StatModifier modifier in character.multiplyingBonuses)
			this.multiplyingBonuses.Add(modifier);

		StatsDictionary.Add(Stats.HP, new VariableReference(() => Hp, val => { Hp = (float)val; }));
		StatsDictionary.Add(Stats.HP_MAX, new VariableReference(() => HpMax, val => { HpMax = (float)val; }));
	}*/


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

	public void ModifyStats(Stats stat, float value)
	{
		StatsDictionary[stat].Set((float)StatsDictionary[stat].Get() + value);
	}

}

[System.Serializable]
public class StatModifier
{
	public int modifierFromID;
	public Stats modifierTo;
	public string description;
	public float value;
}
