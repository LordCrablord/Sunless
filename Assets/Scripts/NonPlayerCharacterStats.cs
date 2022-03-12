using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New NPC", menuName = "Character/NPC")]
public class NonPlayerCharacterStats : CharacterStats
{
	public int npcID;
    /*NonPlayerCharacterStats(NonPlayerCharacterStats npc)
	{
		this.npcID = npc.npcID;
	}

	public NonPlayerCharacterStats Clone()
	{
		return new NonPlayerCharacterStats(this);
	}*/
}
