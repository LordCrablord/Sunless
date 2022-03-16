using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New SpellAbility", menuName = "Ability/SpellAbility")]
public class SpellAbility : Ability
{
    public bool autoHit = false;
    public float minDamage;
    public float maxDamage;
    public Stats damageType;
    public float cooldown;
    public List<AbilityCondition> conditions;

    public override void DoAbility(CharacterStats actionOriginator, Ability ability, CharacterStats initialTarget)
	{

	}
}

[System.Serializable]
public class AbilityCondition
{
    public string conditionName;
    public Sprite sprite;
    public float baseSaveDC;
    public List<Scaling> saveDCScaling;
    public float duration;
    public List<ConditionValues> conditionValues;
}

[System.Serializable]
public class Scaling
{
    public Stats stat;
    public float statMod;
}

[System.Serializable]
public class ConditionValues
{
    public Stats stat;
    public float addVal;
    public float multVal;
    public bool hasBaseValue = false;
    public Stats baseValue;
}
