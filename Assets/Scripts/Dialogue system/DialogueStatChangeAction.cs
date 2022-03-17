using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueStatChangeAction : IDialogueAction
{
	DialogueStatChangeData currentData;
	DialogueStatChangeDataItem currentItem;
	public void DoAction(GameObject dialogueManager)
	{
		Stats myStatEnum = (Stats)Enum.Parse(typeof(Stats), currentItem.stat);
		GameManager.Instance.ModifyMainCharacterStat(myStatEnum, currentItem.value);
	}

	public List<DialogueAction> GetFutureDialogueActions()
	{
		throw new System.NotImplementedException();
	}

	public DialogueStatChangeAction(TextAsset dialoguesDataJSON, int jsonId)
	{
		currentData = JsonUtility.FromJson<DialogueStatChangeData>(dialoguesDataJSON.text);
		currentItem = currentData.items.Find(t => t.id == jsonId);
	}
}
