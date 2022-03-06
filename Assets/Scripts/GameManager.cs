using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject characterUI;
    
    public void SetCharacterDataOnUI(PlayerCharacterStats stats)
	{
        characterUI.GetComponent<CharacterUI>().SetCharacterUI(stats);
	}
}
