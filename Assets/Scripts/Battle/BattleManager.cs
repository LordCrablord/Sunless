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

	BattleData currentBattle;

	Queue<CharacterStats> turnOrder;
	int roundCount;
	CharacterStats currentCharacter;
	public CharacterStats CurrentCharacter { get { return currentCharacter; } }
	public Ability selectedAbility;

	[SerializeField] float NPCThinkingTime;

	public void PrepareBattle(BattleData battleData)
	{
		roundCount = 1;
		currentBattle = battleData;
		SetEnemies();
		playerPCs = GameManager.Instance.GetPlayerParty();
		for (int i = 0; i < playerPCs.Length; i++)
		{
			battleUI.SetAllyToken(playerPCs[i], i);
		}
		battleUI.SetBattleStart();
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
		battleUI.SetStartTurnTitle();
	}

	public event EventHandler<int> NewRoundStarted;
	protected virtual void OnNewRoundStarted()
	{
		NewRoundStarted?.Invoke(this, roundCount);
	}

	void SetEnemies()
	{
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
		if (currentBattle.enemies[pos] != null)
		{
			enemies[pos] = Instantiate(currentBattle.enemies[pos]);
			enemies[pos].PrepareStats();
		}
		else enemies[pos] = null;
	}

	void CloneAllyToPosition(int pos)
	{
		if (playerPCs[pos] != null)
		{
			playerPCs[pos] = Instantiate(playerPCs[pos]);
		}
			
		else playerPCs[pos] = null;
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

	public event Notify NPCThinkingFinished;
	protected virtual void OnNPCThinkingFinished()
	{
		NPCThinkingFinished?.Invoke();
	}

	public void StartThinking()
	{
		StartCoroutine("NPCThinking");
	}

	IEnumerator NPCThinking()
	{
		yield return new WaitForSeconds(NPCThinkingTime);
		OnNPCThinkingFinished();
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
		battleUI.SetCharacterUI();
	}

	public void RemoveCharacter(CharacterStats character)
	{
		foreach(CharacterStats characterStats in playerPCs)
		{
			if(characterStats == character)
			{
				playerPCs[characterStats.Position] = null;
				break;
			}
		}
		foreach (CharacterStats characterStats in enemies)
		{
			if (characterStats == character)
			{
				enemies[characterStats.Position] = null;
				break;
			}
		}
		turnOrder = new Queue<CharacterStats>(turnOrder.Where(c => c != character));

		if(CheckIfEmpty(playerPCs))
		{
			BattleLost();
		}else if (CheckIfEmpty(enemies))
		{
			BattleWon();
		}
	}

	bool CheckIfEmpty(CharacterStats[] statsArr)
	{
		foreach(CharacterStats stats in statsArr)
		{
			if (stats != null)
				return false;
		}
		return true;
	}

	void BattleWon()
	{
		battleUI.SetEndBattleAnimation();
		if (currentBattle.dialogueAfterFight != null)
		{
			GameManager.Instance.StartDialogue(currentBattle.dialogueAfterFight);
		}
	}

	void BattleLost()
	{
		battleUI.SetEndBattleAnimation();
	}
}
