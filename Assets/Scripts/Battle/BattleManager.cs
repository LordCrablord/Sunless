using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
	[SerializeField] BattleUI battleUI;
    public PlayerCharacterStats[] playerPCs = new PlayerCharacterStats[3];
	public NonPlayerCharacterStats[] enemies = new NonPlayerCharacterStats[3];

	public BattleData temp;
	BattleData currrentBattle;

	private void Start()
	{
		Invoke("StartLate", 0.1f);
		SetEnemies(temp);
	}
	private void StartLate()
	{
		playerPCs[0] = GameManager.Instance.MainCharacter.CharacterStats;
		battleUI.SetAllyToken(playerPCs[0], 0);
	}

	void SetEnemies(BattleData data)
	{
		currrentBattle = data;
		enemies[0] = currrentBattle.enemies[0];
		enemies[1] = currrentBattle.enemies[1];
		enemies[2] = currrentBattle.enemies[2];

		for(int i = 0; i < enemies.Length; i++)
		{
			battleUI.SetEnemyToken(enemies[i], i);
		}
	}
}
