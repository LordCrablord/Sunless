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
		
	}

	void SetButtons()
	{
		List<GameObject> buttons = dialogueManager.GetComponent<DialogueManager>().GetButtons();

		foreach(DialogueBranch branch in currentItem.branches)
		{
			buttons[branch.branch_id].SetActive(true);
		}
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
