using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDialogueAction : IDialogueAction
{
	DialogueData currentData;
	DialogueDataItem currentItem;
	public void DoAction(GameObject dialogueManager)
	{
		dialogueManager.GetComponent<DialogueManager>().PrepareUIForDialogue(currentItem.dialogue);
	}

	public List<DialogueDataItemString> GetCurrentDialogue()
	{
		return currentItem.dialogue;
	}

	public List<DialogueAction> GetDialogueActions()
	{
		return currentItem.actions;
	}

	public SimpleDialogueAction(TextAsset dialoguesDataJSON, int jsonId)
	{
		currentData = JsonUtility.FromJson<DialogueData>(dialoguesDataJSON.text);
		currentItem = currentData.items.Find(t => t.id == jsonId);
	}
}
