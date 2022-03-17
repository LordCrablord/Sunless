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
			if (CheckConditionIfAllowed(branch.conditions))
			{
				buttons[branch.branch_id].SetActive(true);
				buttons[branch.branch_id].GetComponentInChildren<TextMeshProUGUI>().text = branch.text;
				buttons[branch.branch_id].GetComponent<Button>().onClick.AddListener(delegate { OnBranchChosen(branch.branch_id); });
			}
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

	bool CheckConditionIfAllowed(List<BranchCondition> conditions)
	{
		foreach (BranchCondition condition in conditions)
		{
			switch (condition.cond_type)
			{
				case BranchConditionType.QUEST_PRESENT:
					if (!QuestManager.Instance.TriggerManager.questPartAllowID.Contains(condition.cond_val))
						return false;
					break;
				case BranchConditionType.QUEST_NOT_PRESENT:
					if (QuestManager.Instance.TriggerManager.questPartAllowID.Contains(condition.cond_val))
						return false;
					break;
				case BranchConditionType.EVENT_PRESENT:
					if (QuestManager.Instance.TriggerManager.settlementConditionForbidID.Contains(condition.cond_val))
						return false;
					break;
				case BranchConditionType.EVENT_NOT_PRESENT:
					if (!QuestManager.Instance.TriggerManager.settlementConditionForbidID.Contains(condition.cond_val))
						return false;
					break;
				case BranchConditionType.STR:
					if (GameManager.Instance.GetMainCharacter().Str<condition.cond_val)
						return false;
					break;
				case BranchConditionType.DEX:
					if (GameManager.Instance.GetMainCharacter().Dex < condition.cond_val)
						return false;
					break;
				case BranchConditionType.CON:
					if (GameManager.Instance.GetMainCharacter().Con < condition.cond_val)
						return false;
					break;
				case BranchConditionType.INT:
					if (GameManager.Instance.GetMainCharacter().Int < condition.cond_val)
						return false;
					break;
			}
		}
		return true;
	}
}
