using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    float actionWaiting = 0.15f;

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
        Debug.Log("Arrrrrr");
	}
}
