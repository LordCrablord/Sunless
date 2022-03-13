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

	[SerializeField] TextMeshProUGUI characterNameTMP;
	[SerializeField] Image image;
	[SerializeField] TextMeshProUGUI hpTMP;
	[SerializeField] Slider healthSlider;
	[SerializeField] TextMeshProUGUI armorTMP;
	[SerializeField] TextMeshProUGUI actionPointsTMP;

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
		BattleManager.Instance.DoNextTurn();
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
			PlayerCharacterStats playerCharacter = (PlayerCharacterStats)BattleManager.Instance.CurrentCharacter;
			characterNameTMP.text = playerCharacter.characterName;
			image.sprite = playerCharacter.sprite;
			hpTMP.text = playerCharacter.Hp + "/" + playerCharacter.HpMax;
			healthSlider.maxValue = playerCharacter.HpMax;
			healthSlider.value = playerCharacter.Hp;
			armorTMP.text = playerCharacter.ArmorClass.ToString();
			actionPointsTMP.text = "AP: " + playerCharacter.Ap + " / " + playerCharacter.ApMax;
			//ManageMoveButtons();
		}
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
}
