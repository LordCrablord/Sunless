using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class BattleUI : MonoBehaviour
{
    public GameObject[] allyTilesTokenUI = new GameObject[3];
	public GameObject[] enemyTilesTokenUI = new GameObject[3];
	[SerializeField] GameObject tokenPrefab;
	[SerializeField] TextMeshProUGUI roundTMP;
	private void Start()
	{
		BattleManager.Instance.NewRoundStarted += NewRound;
	}

	public void SetToken(CharacterStats stat, int position, GameObject[] destinationTiles)
	{
		GameObject token = Instantiate(tokenPrefab, transform.position, Quaternion.identity);
		token.GetComponent<Token>().SetToken(stat);
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
		BattleManager.Instance.MakeNextTurn();
	}

	void NewRound(object sender, int roundCount)
	{
		roundTMP.text = "Round: " + roundCount;
	}
}
