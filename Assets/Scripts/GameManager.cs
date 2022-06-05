using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] ItemDatabase itemDatabase;
    public ItemDatabase ItemDatabase {get { return itemDatabase; }}

    [SerializeField] BattleDatabase battleDatabase;
    public BattleDatabase BattleDatabase { get { return battleDatabase; } }

    [SerializeField] AbilityDatabase pcAbilityDatabase;
    public AbilityDatabase PCAbilityDatabase { get { return pcAbilityDatabase; } }

    [SerializeField] PartyManager partyManager;

    [SerializeField] GameObject characterUI;
    [SerializeField] GameObject dialogueManager;
    [SerializeField] GameObject unitsContainer;
    [SerializeField] GameObject pauseUI;
    [SerializeField] GameObject escapekeyUI;
    bool charatcerUIActive = false;

    int pauseGameRequest = 0;
    public int PauseGameRequest
	{
		get { return pauseGameRequest; }
		set 
        {
            pauseGameRequest = value;
            if (pauseGameRequest > 0)
                OnGamePaused();
            else
                OnGameResumed();
        }
	}

    public event Notify GamePaused;
    protected virtual void OnGamePaused()
    {
        pauseUI.SetActive(true);
        GamePaused?.Invoke();
    }

    public event Notify GameResumed;
    protected virtual void OnGameResumed()
    {
        pauseUI.SetActive(false);
        GameResumed?.Invoke();
    }

	public void SetCharacterDataOnUI(PlayerCharacterStats stats)
	{
        characterUI.GetComponent<CharacterUI>().SetCharacterUI(stats);
	}

    public void ToggleCharacterUI(PlayerCharacterStats stats)
	{
        charatcerUIActive = !charatcerUIActive;
        characterUI.SetActive(charatcerUIActive);
        if (charatcerUIActive) SetCharacterDataOnUI(stats);
    }

    public void StartDialogue(DialogueAction dialogueAction)
	{
        dialogueManager.GetComponent<DialogueManager>().StartDialogue(dialogueAction);
	}

    public PlayerCharacterStats[] GetPlayerParty()
	{
        return partyManager.party;
	}

    public void ModifyMainCharacterStat(Stats stat, float value)
	{
        partyManager.ModifyMainCharacterStats(stat, value);
    }

    public PlayerCharacterStats GetMainCharacter()
	{
        return partyManager.MainCharacter;
	}

    public PartyManager GetPartyManager()
	{
        return partyManager;
	}

    public void ToggleEscapeKeyUI()
	{
        escapekeyUI.SetActive(!escapekeyUI.activeInHierarchy);
	}
}
