using System;
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
	int roundCount;
	CharacterStats currentCharacter;
	public CharacterStats CurrentCharacter { get { return currentCharacter; } }
	public Ability selectedAbility;
	/*{
		get { return selectedAbility; }
		set
		{
			selectedAbility = value;
			Debug.Log(selectedAbility.abilityName + " is selected");
		}
	}*/

	private void Start()
	{
		roundCount = 0;
		Invoke("StartLate", 0.1f);
		SetEnemies(temp);
		
	}
	//at the moment late start as testing and it was not really instatiated yet
	//it wont be a problem later, so late start can be removed, later, after testing
	private void StartLate()
	{
		playerPCs[0] = GameManager.Instance.MainCharacter.CharacterStats;
		CloneAllyToPosition(2);
		for (int i = 0; i < playerPCs.Length; i++)
		{
			battleUI.SetAllyToken(playerPCs[i], i);
		}
		

		SetTurnOrder();
		DoNextTurn();
	}

	public void DoNextTurn()
	{
		turnOrder = new Queue<CharacterStats>(turnOrder.OrderBy(q => q.Initiative).Reverse());
		if (turnOrder.Count == 0)
		{
			roundCount++;
			OnNewRoundStarted();
			SetTurnOrder();
		}
		currentCharacter = turnOrder.Dequeue();
		currentCharacter.OnTurnStarted();
		battleUI.SetCharacterUI();
	}

	public event EventHandler<int> NewRoundStarted;
	protected virtual void OnNewRoundStarted()
	{
		NewRoundStarted?.Invoke(this, roundCount);
	}

	void SetEnemies(BattleData data)
	{
		currrentBattle = data;
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

	void CloneAllyToPosition(int pos)
	{
		if (playerPCs[pos] != null)
		{
			playerPCs[pos] = Instantiate(playerPCs[pos]);
		}
			
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

	public void AbilitySelect(Ability ability)
	{
		selectedAbility = ability;
		Debug.Log(selectedAbility.abilityName + " is selected");

		battleUI.ManageSelection();
	}

	public void AttemptAbilityAction(CharacterStats actionOriginator, Ability ability, CharacterStats initialTarget)
	{
		battleUI.ClearSelection();

		/*CharacterStats[] targets;
		switch (actionOriginator)
		{
			case PlayerCharacterStats pp:
				targets = enemies;
				break;
			case NonPlayerCharacterStats npp:
				targets = playerPCs;
				break;
		}*/

		if (ability.actionOnAllTargets == true)
		{
			foreach (TargetPosition pos in ability.targetEnemy)
				ability.DoAbility(actionOriginator, ability, enemies[(int)pos]);
			foreach (TargetPosition pos in ability.targetAlly)
				ability.DoAbility(actionOriginator, ability, playerPCs[(int)pos]);
		}
		else
		{
			ability.DoAbility(actionOriginator, ability, initialTarget);
		}
		actionOriginator.Ap -= ability.apCost;
		
		
	}
}
