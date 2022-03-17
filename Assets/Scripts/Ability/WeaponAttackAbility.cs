using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New WeaponAttackAbility", menuName = "Ability/WeaponAttackAbility")]
public class WeaponAttackAbility : Ability
{
    public float baseDmgValueAddMod;
    public float baseDmgValueMultMod = 1;
    public float baseCrtChanceAddMod;
    public float baseCrtChanceMultMod;
    public float baseCrtValueAddMod;
    public float baseCrtValueMultMod = 1;

    public override void DoAbility(CharacterStats actionOriginator, Ability ability, CharacterStats initialTarget)
    {
        Debug.Log(abilityName + " is used on " + initialTarget.characterName);
        actionOriginator.OnActionStarted(ability.abilityName);
        PlayerCharacterStats originator = (PlayerCharacterStats)actionOriginator;
        NonPlayerCharacterStats target = (NonPlayerCharacterStats)initialTarget;

        MakeAnAttack(originator, target);

        var currEchoVal = echoingToNextTarget;
		while (currEchoVal != 0)
		{
            currEchoVal--;
            target = BattleManager.Instance.enemies[initialTarget.Position + echoingToNextTarget - currEchoVal];
            if(target != null)
                MakeAnAttack(originator, target);
        }
    }

    void MakeAnAttack(PlayerCharacterStats originator, NonPlayerCharacterStats target)
    {
        //TODO evasion
        //TODO bonus to hit
        var damageMin = (originator.DamageMin + baseDmgValueAddMod) * baseDmgValueMultMod;
        var damageMax = (originator.DamageMax + baseDmgValueAddMod) * baseDmgValueMultMod;
        var damage = Random.Range(damageMin, damageMax);
        var critChance = originator.CritChance + baseCrtChanceAddMod;
        var critValue = (originator.CritValue + baseCrtValueAddMod) * baseCrtValueMultMod;

        bool isCrit = false;
        int currentCritChance = Random.Range(0, 100);
        if (currentCritChance <= critChance)
		{
            isCrit = true;
            damage *= critValue;
            Debug.Log("Crit against " + target.name);
        }   

        damage -= (float)target.StatsDictionary[originator.weapon.DamageDestination].Get();
        if (damage < 0) damage = 0;

        damage = Mathf.RoundToInt(damage);
        target.Hp = target.Hp - damage;
        target.OnDamaged(new DamageEventArgs(damage, isCrit));
    }
}

public class DamageEventArgs
{
    public float damage;
    public bool isCrit;

	public DamageEventArgs(float dmg, bool crit)
	{
        damage = dmg;
        isCrit = crit;
	}
}
