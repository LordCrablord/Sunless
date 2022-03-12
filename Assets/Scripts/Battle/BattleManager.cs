using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
	[SerializeField] BattleUI battleUI;
    public PlayerCharacterStats[] playerPCs = new PlayerCharacterStats[3];

	private void Start()
	{
		battleUI.SetToken(playerPCs[0], 0);
	}
}
