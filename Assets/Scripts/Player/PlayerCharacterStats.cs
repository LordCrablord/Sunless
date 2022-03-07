using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterStats : CharacterStats
{
    public int level;
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
				); ;
		}
	}
}
