using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject characterUI;
    
    public void SetCharacterDataOnUI(CharacterStats stats)
	{
        characterUI.GetComponent<CharacterUI>().SetCharacterUI(stats);
	}
}
