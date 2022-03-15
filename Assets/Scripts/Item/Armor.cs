using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArmorWeight { LIGHT, MEDIUM, HEAVY }

[CreateAssetMenu(fileName = "New Armor", menuName = "Items/Armor")]
public class Armor : Item
{
    public ArmorWeight armorWeight;
    public int armorValue;

    [Header("Armor values")]
    [SerializeField] float protPierce;
    [SerializeField] float protSlash;
    [SerializeField] float protBludge;
    [SerializeField] float protElement;
    [SerializeField] float protEldrich;
    [SerializeField] float protArcane;

    public Dictionary<Stats, VariableReference> armorProtections = new Dictionary<Stats, VariableReference>();
    bool dictInitialized = false;

	public float GetArmorProtVal(Stats stat)
	{
		if (!dictInitialized)
		{
            armorProtections.Add(Stats.PROT_PIERCE, new VariableReference(() => protPierce, val => { protPierce = (float)val; }));
            armorProtections.Add(Stats.PROT_SLASH, new VariableReference(() => protSlash, val => { protSlash = (float)val; }));
            armorProtections.Add(Stats.PROT_BLUDGE, new VariableReference(() => protBludge, val => { protBludge = (float)val; }));
            armorProtections.Add(Stats.PROT_ELEMENT, new VariableReference(() => protElement, val => { protElement = (float)val; }));
            armorProtections.Add(Stats.PROT_ELDRICH, new VariableReference(() => protEldrich, val => { protEldrich = (float)val; }));
            armorProtections.Add(Stats.PROT_ARCANE, new VariableReference(() => protArcane, val => { protArcane = (float)val; }));
            dictInitialized = true;
        }

        return (float)armorProtections[stat].Get();
    }
   
}
