using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleDialogueTrigger : MonoBehaviour
{
    [SerializeField] GameObject interactableUI;
    [SerializeField] DialogueAction dialogueAction;
    
	private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.name == "PlayerCharacter")
        {
            other.gameObject.GetComponent<PlayerController>().EventTriggered += OnEventTriggered;
            interactableUI.SetActive(true);
        }
    }

	private void OnTriggerExit(Collider other)
	{
        if (other.gameObject.name == "PlayerCharacter")
        {
            other.gameObject.GetComponent<PlayerController>().EventTriggered -= OnEventTriggered;
            interactableUI.SetActive(false);
        }
    }

	void OnEventTriggered()
	{
        GameManager.Instance.StartDialogue(dialogueAction);
	}
}
