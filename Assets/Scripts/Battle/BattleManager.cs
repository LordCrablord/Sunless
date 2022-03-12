using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleManager : Singleton<BattleManager>
{
	[SerializeField] BattleUI battleUI;
    public PlayerCharacterStats[] playerPCs = new PlayerCharacterStats[3];
	public NonPlayerCharacterStats[] enemies = new NonPlayerCharacterStats[3];

	public BattleData temp;
	BattleData currrentBattle;

	Queue<CharacterStats> turnOrder;

	private void Start()
	{
		Invoke("StartLate", 0.1f);
		SetEnemies(temp);
		
	}
	//at the moment late start as testing and it was not really instatiated yet
	//it wont be a problem later, so late start can be removed, later, after testing
	private void StartLate()
	{
		playerPCs[0] = GameManager.Instance.MainCharacter.CharacterStats;
		battleUI.SetAllyToken(playerPCs[0], 0);

		SetTurnOrder();
		foreach (CharacterStats cha in turnOrder)
			Debug.Log(cha.characterName);
		turnOrder.Dequeue().OnTurnStarted();
	}

	public void MakeNextTurn()
	{
		turnOrder = new Queue<CharacterStats>(turnOrder.OrderBy(q => q.Initiative).Reverse());
		if (turnOrder.Count == 0)
		{
			Debug.Log("New Round");
			SetTurnOrder();
		}
		turnOrder.Dequeue().OnTurnStarted();
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

	void SetTurnOrder()
	{
		turnOrder = new Queue<CharacterStats>();
		foreach (PlayerCharacterStats pcStat in playerPCs)
		{
			if(pcStat!=null)
				turnOrder.Enqueue(pcStat);
		}
		foreach (NonPlayerCharacterStats npcStat in enemies)
		{
			if (npcStat != null)
				turnOrder.Enqueue(npcStat);
		}
		turnOrder = new Queue<CharacterStats>(turnOrder.OrderBy(q=>q.Initiative).Reverse());
	}
}
