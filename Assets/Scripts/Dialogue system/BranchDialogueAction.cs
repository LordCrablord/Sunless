using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BranchDialogueAction : IDialogueAction
{
	DialogueBranchesData currentData;
	DialogueBranchesDataItem currentItem;
	GameObject dialogueManager;
	List<DialogueAction> actions;
	public void DoAction(GameObject manager)
	{
		this.dialogueManager = manager;
		dialogueManager.GetComponent<DialogueManager>().ResetButtons();
		SetButtons();
		dialogueManager.GetComponent<DialogueManager>().StartDialogueSetup(currentItem.dialogue);
	}

	void SetButtons()
	{
		List<GameObject> buttons = dialogueManager.GetComponent<DialogueManager>().GetButtons();

		foreach(DialogueBranch branch in currentItem.branches)
		{
			buttons[branch.branch_id].SetActive(true);
			buttons[branch.branch_id].GetComponentInChildren<TextMeshProUGUI>().text = branch.text;
			buttons[branch.branch_id].GetComponent<Button>().onClick.AddListener(delegate { OnBranchChosen(branch.branch_id); });
		}
	}

	public void OnBranchChosen(int id)
	{
		actions = currentItem.branches.Find(b => b.branch_id == id).actions;
		dialogueManager.GetComponent<DialogueManager>().ManageActions();
	}

	public List<DialogueAction> GetFutureDialogueActions()
	{
		return actions;
	}

	public BranchDialogueAction(TextAsset dialoguesDataJSON, int jsonId)
	{
		currentData = JsonUtility.FromJson<DialogueBranchesData>(dialoguesDataJSON.text);
		currentItem = currentData.items.Find(t => t.id == jsonId);
	}
}
