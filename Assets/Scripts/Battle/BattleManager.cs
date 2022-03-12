using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
	[SerializeField] BattleUI battleUI;
    public PlayerCharacterStats[] playerPCs = new PlayerCharacterStats[3];

	private void Start()
	{
		Invoke("StartLate", 0.1f);
	}
	private void StartLate()
	{
		playerPCs[0] = GameManager.Instance.MainCharacter.CharacterStats;
		battleUI.SetToken(playerPCs[0], 0);
	}
}
