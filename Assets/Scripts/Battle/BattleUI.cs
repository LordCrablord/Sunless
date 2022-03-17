using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public GameObject[] allyTilesTokenUI = new GameObject[3];
	public GameObject[] enemyTilesTokenUI = new GameObject[3];
	[SerializeField] GameObject tokenPrefab;
	[SerializeField] TextMeshProUGUI roundTMP;
	[SerializeField] Button moveLeftButton;
	[SerializeField] Button moveRightButton;
	[SerializeField] TextMeshProUGUI startTurnTitleTMP;

	[Header ("Lower Left")]
	[SerializeField] TextMeshProUGUI characterNameTMP;
	[SerializeField] Image image;
	[SerializeField] TextMeshProUGUI hpTMP;
	[SerializeField] Slider healthSlider;
	[SerializeField] TextMeshProUGUI actionPointsTMP;
	[SerializeField] GameObject bottomPanelBlocker;

	[Header("Armor")]
	[SerializeField] TextMeshProUGUI protPierceTMP;
	[SerializeField] TextMeshProUGUI protSlashTMP;
	[SerializeField] TextMeshProUGUI protBludgeTMP;
	[SerializeField] TextMeshProUGUI protElementTMP;
	[SerializeField] TextMeshProUGUI protEldrichTMP;
	[SerializeField] TextMeshProUGUI protArcaneTMP;

	[Header("Lower right")]
	[SerializeField] GameObject weaponAbilityMain;
	[SerializeField] GameObject weaponAbilitySecond;
	[SerializeField] List<GameObject> abilitiesList;

	private void Start()
	{
		BattleManager.Instance.NewRoundStarted += NewRound;
	}

	public void SetToken(CharacterStats stat, int position, GameObject[] destinationTiles)
	{
		GameObject token = Instantiate(tokenPrefab, transform.position, Quaternion.identity);
		token.GetComponent<Token>().SetToken(stat, position);
		token.transform.SetParent(destinationTiles[position].transform, false);
		token.transform.localPosition = Vector3.zero;
	}

	public void SetAllyToken(CharacterStats stat, int position)
	{
		if (stat == null) return;
		SetToken(stat, position, allyTilesTokenUI);
	}

	public void SetEnemyToken(CharacterStats stat, int position)
	{
		if (stat == null) return;
		SetToken(stat, position, enemyTilesTokenUI);
	}

	public void OnTurnEndButtonClicked()
	{
		BattleManager.Instance.CurrentCharacter.OnTurnEnded();
	}

	public void OnMoveButtonClicked(int value)
	{

		int newPos = BattleManager.Instance.CurrentCharacter.Position + value;
		if(newPos>=0 && newPos < 3)
		{

			//swap in manager
			GameObject[] tokens;
			//change later
			tokens = allyTilesTokenUI;

			GameObject secondCharToken = null;
			if (tokens[newPos].transform.childCount > 0) secondCharToken = tokens[newPos].transform.GetChild(0).gameObject;

			GameObject myCharToken = tokens[BattleManager.Instance.CurrentCharacter.Position].transform.GetChild(0).gameObject;
			myCharToken.transform.SetParent(tokens[newPos].transform, false);
			myCharToken.transform.localPosition = Vector3.zero;
			int firstPos = BattleManager.Instance.CurrentCharacter.Position;
			BattleManager.Instance.CurrentCharacter.Position = newPos;

			if (secondCharToken != null)
			{
				secondCharToken.transform.SetParent(tokens[firstPos].transform, false);
				secondCharToken.transform.localPosition = Vector3.zero;
				secondCharToken.GetComponent<Token>().GetStat().Position = firstPos;
			}

			PlayerCharacterStats temp = BattleManager.Instance.playerPCs[firstPos];
			BattleManager.Instance.playerPCs[firstPos] = BattleManager.Instance.playerPCs[newPos];
			BattleManager.Instance.playerPCs[newPos] = temp;

			BattleManager.Instance.CurrentCharacter.Ap--;
			SetCharacterUI();
		}
		
	}

	void NewRound(object sender, int roundCount)
	{
		roundTMP.text = "Round: " + roundCount;
	}

	public void SetCharacterUI()
	{
		if (BattleManager.Instance.CurrentCharacter is PlayerCharacterStats)
		{
			bottomPanelBlocker.SetActive(false);
			PlayerCharacterStats playerCharacter = (PlayerCharacterStats)BattleManager.Instance.CurrentCharacter;
			characterNameTMP.text = playerCharacter.characterName;
			image.sprite = playerCharacter.sprite;
			hpTMP.text = playerCharacter.Hp + "/" + playerCharacter.HpMax;
			healthSlider.maxValue = playerCharacter.HpMax;
			healthSlider.value = playerCharacter.Hp;

			protPierceTMP.text = playerCharacter.ProtPierce.ToString();
			protSlashTMP.text = playerCharacter.ProtSlash.ToString();
			protBludgeTMP.text = playerCharacter.ProtBludge.ToString();
			protElementTMP.text = playerCharacter.ProtElement.ToString();
			protEldrichTMP.text = playerCharacter.ProtEldrich.ToString();
			protArcaneTMP.text = playerCharacter.ProtArcane.ToString();

			for(int i = 0; i<playerCharacter.ActiveAbilities.Count; i++)
			{
				abilitiesList[i].GetComponent<AbilityUIHolder>().SetAbilityUI(playerCharacter.ActiveAbilities[i], playerCharacter.Position);
			}

			actionPointsTMP.text = "AP: " + playerCharacter.Ap + " / " + playerCharacter.ApMax;

			if (playerCharacter.weapon != null)
			{
				weaponAbilityMain.GetComponent<WeaponAbilityUIHolder>().SetWeaponAbilityUI(playerCharacter.weapon.mainAbility, playerCharacter.Position);
				weaponAbilitySecond.GetComponent<WeaponAbilityUIHolder>().SetWeaponAbilityUI(playerCharacter.weapon.secondAbility, playerCharacter.Position);
			}
			else
			{
				weaponAbilityMain.GetComponent<WeaponAbilityUIHolder>().ClearWeaponAbilityUI();
				weaponAbilitySecond.GetComponent<WeaponAbilityUIHolder>().ClearWeaponAbilityUI();
			}
		}
		else
		{
			bottomPanelBlocker.SetActive(true);
		}
	}

	public void ManageSelection()
	{
		ClearSelection();
		foreach(TargetPosition pos in BattleManager.Instance.selectedAbility.targetEnemy)
		{
			if (enemyTilesTokenUI[(int)pos].transform.childCount > 0)
			{
				enemyTilesTokenUI[(int)pos].transform.GetChild(0).GetComponent<Token>().SetSelectedStatus(true);
			}
		}
		foreach (TargetPosition pos in BattleManager.Instance.selectedAbility.targetAlly)
		{
			if (allyTilesTokenUI[(int)pos].transform.childCount > 0)
			{
				allyTilesTokenUI[(int)pos].transform.GetChild(0).GetComponent<Token>().SetSelectedStatus(true);
			}
		}
	}

	public void ClearSelection()
	{
		//TODO later something with ability clear too? like if(clearAbilityToo) BatleManager.Instance.abilitySelected = null;
		foreach(GameObject holder in enemyTilesTokenUI)
		{
			if (holder.transform.childCount > 0)
			{
				holder.transform.GetChild(0).GetComponent<Token>().SetSelectedStatus(false);
			}
		}
		foreach (GameObject holder in allyTilesTokenUI)
		{
			if (holder.transform.childCount > 0)
			{
				holder.transform.GetChild(0).GetComponent<Token>().SetSelectedStatus(false);
			}
		}
	}

	public void SetStartTurnTitle()
	{
		startTurnTitleTMP.text = BattleManager.Instance.CurrentCharacter.characterName + "`s turn";
		GetComponent<Animator>().Play("Turn Started", -1, 0f);
	}

	//this will be later used in setUIForCharacter
	void ManageMoveButtons()
	{
		if (BattleManager.Instance.CurrentCharacter.Position != 0)
			moveRightButton.interactable = true;
		else moveRightButton.interactable = false;
		if (BattleManager.Instance.CurrentCharacter.Position != 2)
			moveLeftButton.interactable = true;
		else moveLeftButton.interactable = false;
	}

	public void SetEndBattleAnimation()
	{
		GetComponent<Animator>().Play("End Battle");
	}

	public void OnEndBattleAnimationFinished()
	{
		gameObject.SetActive(false);
	}

	public void SetBattleStart()
	{
		gameObject.SetActive(true);
		GetComponent<Animator>().Play("Start Battle");
	}
}
