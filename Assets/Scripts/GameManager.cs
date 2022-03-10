using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] ItemDatabase itemDatabase;
    public ItemDatabase ItemDatabase {get { return itemDatabase; }}

    [SerializeField] PlayerController mainCharacter;
    public PlayerController MainCharacter { get { return mainCharacter; } }

    [SerializeField] GameObject characterUI;
    [SerializeField] GameObject dialogueManager;
    [SerializeField] GameObject unitsContainer;

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
        GamePaused?.Invoke();
    }

    public event Notify GameResumed;
    protected virtual void OnGameResumed()
    {
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
}
