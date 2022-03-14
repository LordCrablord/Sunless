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
        PlayerCharacterStats originator = (PlayerCharacterStats)actionOriginator;
        NonPlayerCharacterStats target = (NonPlayerCharacterStats)initialTarget;

        MakeAnAttack(originator, target);

        var currEchoVal = echoingToNextTarget;
		while (currEchoVal != 0)
		{
            currEchoVal--;
            target = BattleManager.Instance.enemies[initialTarget.Position + echoingToNextTarget - currEchoVal];
            MakeAnAttack(originator, target);
        }
    }

    void MakeAnAttack(PlayerCharacterStats originator, NonPlayerCharacterStats target)
    {
        //TODO evasion
        //TODO bonus to hit
        var damage = (originator.Damage + baseDmgValueAddMod) * baseDmgValueMultMod;
        var critChance = originator.CritChance + baseCrtChanceAddMod;
        var critValue = (originator.CritValue + baseCrtValueAddMod) * baseCrtValueMultMod;

        int currentCritChance = Random.Range(0, 100);
        if (currentCritChance <= critChance)
		{
            damage *= critValue;
            Debug.Log("Crit against " + target.name);
        }
            

        //TODO normal armor Reduction, but this will do for now
        damage -= target.armorClass;
        damage = Mathf.RoundToInt(damage);
        target.Hp = target.Hp - damage;
    }
}
