using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : MonoBehaviour
{
    public GameObject[] allyTilesTokenUI = new GameObject[3];

    public void SetToken(CharacterStats stat, int position)
	{
		allyTilesTokenUI[position].transform.GetChild(0)
			.gameObject.GetComponent<Token>().SetToken(stat);
	}
}
