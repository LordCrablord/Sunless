using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDialogueAction : IDialogueAction
{
	DialogueRandomData currentData;
	DialogueRandomDataItem currentItem;
	GameObject dialogueManager;
	List<DialogueAction> actions;
	public void DoAction(GameObject manager)
	{
		this.dialogueManager = manager;
		dialogueManager.GetComponent<DialogueManager>().ResetButtons();

		int resActionIndex = Random.Range(0, currentItem.random_options.Count);
		DialogueAction dialogueAction = currentItem.random_options[resActionIndex];
		actions = new List<DialogueAction>();
		actions.Add(dialogueAction);
		dialogueManager.GetComponent<DialogueManager>().ManageActions();
	}

	public RandomDialogueAction(TextAsset dialoguesDataJSON, int jsonId)
	{
		currentData = JsonUtility.FromJson<DialogueRandomData>(dialoguesDataJSON.text);
		currentItem = currentData.items.Find(t => t.id == jsonId);
	}

	public List<DialogueAction> GetFutureDialogueActions()
	{
		return actions;
	}

	
}
