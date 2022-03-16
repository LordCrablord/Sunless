using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[System.Serializable]
[CreateAssetMenu(fileName = "New NPC", menuName = "Character/NPC")]
public class NonPlayerCharacterStats : CharacterStats
{
	public int npcID;
	public int armorClass;

    [Header("Armor values")]
    [SerializeField] float protPierce;
    [SerializeField] float protSlash;
    [SerializeField] float protBludge;
    [SerializeField] float protElement;
    [SerializeField] float protEldrich;
    [SerializeField] float protArcane;

    [SerializeField] List<SpellAbility> abilities;


    public void PrepareStats()
    {
        StatsDictionary.Add(Stats.PROT_PIERCE, new VariableReference(() => protPierce, val => { protPierce = (float)val; }));
        StatsDictionary.Add(Stats.PROT_SLASH, new VariableReference(() => protSlash, val => { protSlash = (float)val; }));
        StatsDictionary.Add(Stats.PROT_BLUDGE, new VariableReference(() => protBludge, val => { protBludge = (float)val; }));
        StatsDictionary.Add(Stats.PROT_ELEMENT, new VariableReference(() => protElement, val => { protElement = (float)val; }));
        StatsDictionary.Add(Stats.PROT_ELDRICH, new VariableReference(() => protEldrich, val => { protEldrich = (float)val; }));
        StatsDictionary.Add(Stats.PROT_ARCANE, new VariableReference(() => protArcane, val => { protArcane = (float)val; }));
    }

	public override void OnTurnStarted()
	{
		base.OnTurnStarted();
        BattleManager.Instance.NPCThinkingFinished += NPCAttackDecision;
        BattleManager.Instance.StartThinking();
	}

    void NPCAttackDecision()
	{
        List<SpellAbility> actionPossibilities = abilities.Where(
            a => a.apCost <= Ap && 
            a.allowedFromPosition.Contains((TargetPosition)Position) &&
            (CheckForPresence(a.targetEnemy, BattleManager.Instance.enemies) || CheckForPresence(a.targetAlly, BattleManager.Instance.playerPCs)) &&
            !abilityCooldowns.ContainsKey(a)
            ).ToList();
		if (actionPossibilities.Count != 0)
		{
            SpellAbility action = actionPossibilities[Random.Range(0, actionPossibilities.Count)];
            List<CharacterStats> targets = new List<CharacterStats>();
			if (action.targetAlly.Count != 0)
			{
                targets = GetInhabitats(action.targetAlly, BattleManager.Instance.playerPCs);
			}
            else if (action.targetEnemy.Count != 0)
            {
                targets = GetInhabitats(action.targetEnemy, BattleManager.Instance.enemies);
            }
			if (targets.Count != 0)
			{
                CharacterStats target = targets[Random.Range(0, targets.Count)];
                BattleManager.Instance.AttemptAbilityAction(this, action, target);
                BattleManager.Instance.StartThinking();
			}
			else
			{
                BattleManager.Instance.NPCThinkingFinished -= NPCAttackDecision;
                OnTurnEnded();
            }
            
        }
		else
		{
            BattleManager.Instance.NPCThinkingFinished -= NPCAttackDecision;
            OnTurnEnded();
        }
	}

    bool CheckForPresence(List<TargetPosition> positions, CharacterStats[] characters)
	{
        foreach(TargetPosition pos in positions)
		{
            if (characters[(int)pos] != null)
                return true;
		}
        return false;
	}

    List<CharacterStats> GetInhabitats(List<TargetPosition> origin, CharacterStats[] characters)
	{
        List<CharacterStats> res = new List<CharacterStats>();
        foreach(TargetPosition or in origin)
		{
            if (characters[(int)or] != null)
                res.Add(characters[(int)or]);
		}
        return res;
	}
}
