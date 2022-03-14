using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetPosition { POS_ZERO, POS_ONE, POS_TWO, NONE, SELF}

[System.Serializable]
[CreateAssetMenu(fileName = "New Ability", menuName = "Ability/Ability")]
public class Ability : ScriptableObject
{
    public int abilityID;
    public string abilityName;
    public string description;
    public Sprite sprite;
    public List<TargetPosition> targetEnemy;
    public List<TargetPosition> targetAlly;
    public bool actionOnAllTargets;
    public List<TargetPosition> allowedFromPosition;
    public int echoingToNextTarget;
    public float echoValMultMod = 1;
    
    public int apCost;

    public virtual void DoAbility(CharacterStats actionOriginator, Ability ability, CharacterStats initialTarget)
	{

	}
}
