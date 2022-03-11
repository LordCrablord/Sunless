using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractibleDialogueTrigger : MonoBehaviour
{
    [SerializeField] GameObject interactableUI;
    [SerializeField] int dialogueQuestConditionID;
    [SerializeField] DialogueAction dialogueAction;
    
	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.name == "PlayerCharacter")
        {
			if (QuestManager.Instance.TriggerManager.questPartAllowID.Contains(dialogueQuestConditionID))
			{
                other.gameObject.GetComponent<PlayerController>().EventTriggered += OnEventTriggered;
                interactableUI.SetActive(true);
            }
            
        }
    }

	private void OnTriggerExit(Collider other)
	{
        if (other.gameObject.name == "PlayerCharacter")
        {
            if (QuestManager.Instance.TriggerManager.questPartAllowID.Contains(dialogueQuestConditionID))
            {
                other.gameObject.GetComponent<PlayerController>().EventTriggered -= OnEventTriggered;
                interactableUI.SetActive(false);
            }
        }
    }

	void OnEventTriggered()
	{
        GameManager.Instance.StartDialogue(dialogueAction);
	}
}

/*[Serializable]
class DialogueConditionProck
{

}*/
