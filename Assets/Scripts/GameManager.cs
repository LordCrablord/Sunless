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

    bool charatcerUIActive = false;
    
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
