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
		/*enemies[0] = Instantiate(currrentBattle.enemies[0]);
		enemies[1] = Instantiate(currrentBattle.enemies[1]);
		enemies[2] = Instantiate(currrentBattle.enemies[2]);*/
		CloneEnemyToPosition(0);
		CloneEnemyToPosition(1);
		CloneEnemyToPosition(2);

		for (int i = 0; i < enemies.Length; i++)
		{
			battleUI.SetEnemyToken(enemies[i], i);
		}
	}

	void CloneEnemyToPosition(int pos)
	{
		if (currrentBattle.enemies[pos] != null)
			enemies[pos] = Instantiate(currrentBattle.enemies[pos]);
		else enemies[pos] = null;
	}
}
