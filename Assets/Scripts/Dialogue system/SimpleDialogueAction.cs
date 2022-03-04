using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDialogueAction : IDialogueAction
{
	DialogueData currentData;
	DialogueDataItem currentItem;
	public void DoAction(GameObject dialogueManager)
	{
		dialogueManager.GetComponent<DialogueManager>().StartDialoguePreparations(currentItem.dialogue);
	}

	public List<DialogueAction> GetFutureDialogueActions()
	{
		return currentItem.actions;
	}

	public SimpleDialogueAction(TextAsset dialoguesDataJSON, int jsonId)
	{
		currentData = JsonUtility.FromJson<DialogueData>(dialoguesDataJSON.text);
		currentItem = currentData.items.Find(t => t.id == jsonId);
	}
}
