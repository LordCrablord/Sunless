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
        bool hit = autoHit;
        if (!autoHit)
		{
            //TODO somewhere evasion and hitting
        }
        hit = MakeAnAttack(actionOriginator, initialTarget);
        if (hit)
		{
            foreach(AbilityCondition abilityCondition in conditions)
			{
                initialTarget.AddToConditions(abilityCondition);
            }
		}
        actionOriginator.abilityCooldowns.Add(this, cooldown);
	}

    bool MakeAnAttack(CharacterStats originator, CharacterStats target)
    {
        //TODO evasion
        //TODO bonus to hit
        var damage = Random.Range(minDamage, maxDamage);

        damage -= (float)target.StatsDictionary[damageType].Get();
        if (damage < 0) damage = 0;

        damage = Mathf.RoundToInt(damage);
        target.Hp = target.Hp - damage;
        target.OnDamaged(new DamageEventArgs(damage, false));
        return true;
    }

    
}

