using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerCharacterStats : CharacterStats
{
    static int xp;
    public int Xp
	{
		get
		{
			return xp;
		}
		set
		{
			xp = Mathf.RoundToInt(
				(value + AddAllBonuses(additiveBonuses, Stats.XP)) + (value + AddAllBonuses(additiveBonuses, Stats.XP)) * AddAllBonuses(multiplyingBonuses, Stats.XP)
				);
			while (xp > levelXpThreshold[level])
			{
				level++;
				Debug.Log(characterName + " is now level " + level);
			}
		}
	}

	public readonly Dictionary<int, int> levelXpThreshold = new Dictionary<int, int>()
	{
		{0, 1},
		{ 1, 100},
		{ 2, 200},
		{ 3, 500},
		{ 4, 800},
	};

	int level;
	public int Level
	{
		get { return level; }
	}
}
