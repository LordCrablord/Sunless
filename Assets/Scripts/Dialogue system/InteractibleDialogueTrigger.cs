using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InteractibleDialogueTrigger : MonoBehaviour
{
    [SerializeField] GameObject interactableUI;
    [SerializeField] bool alwaysActive = false;
    [SerializeField] bool destroyedOnCompletion = true;
    [SerializeField] int dialogueQuestConditionID;
    [SerializeField] DialogueAction dialogueAction;
    
	private void OnTriggerEnter(Collider other)
	{
        
        if (other.gameObject.name == "PlayerCharacter")
        {
            if (alwaysActive)
            {
                other.gameObject.GetComponent<PlayerController>().EventTriggered += OnEventTriggered;
                interactableUI.SetActive(true);
            }else if (QuestManager.Instance.TriggerManager.questPartAllowID.Contains(dialogueQuestConditionID))
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
            if (alwaysActive)
            {
                other.gameObject.GetComponent<PlayerController>().EventTriggered -= OnEventTriggered;
                interactableUI.SetActive(false);
            }
            else if (QuestManager.Instance.TriggerManager.questPartAllowID.Contains(dialogueQuestConditionID))
            {
                other.gameObject.GetComponent<PlayerController>().EventTriggered -= OnEventTriggered;
                interactableUI.SetActive(false);
            }
        }
    }

	void OnEventTriggered()
	{
        GameManager.Instance.StartDialogue(dialogueAction);
        if(destroyedOnCompletion)
            Destroy(gameObject);
	}
}

/*[Serializable]
class DialogueConditionProck
{

}*/
