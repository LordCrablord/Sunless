using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BranchDialogueAction : IDialogueAction
{
	DialogueBranchesData currentData;
	DialogueBranchesDataItem currentItem;
	GameObject dialogueManager;
	public void DoAction(GameObject manager)
	{
		this.dialogueManager = manager;
		Debug.Log(currentItem.branches[0].text);
	}

	public List<DialogueAction> GetFutureDialogueActions()
	{
		throw new System.NotImplementedException();
	}

	public BranchDialogueAction(TextAsset dialoguesDataJSON, int jsonId)
	{
		currentData = JsonUtility.FromJson<DialogueBranchesData>(dialoguesDataJSON.text);
		currentItem = currentData.items.Find(t => t.id == jsonId);
	}
}
