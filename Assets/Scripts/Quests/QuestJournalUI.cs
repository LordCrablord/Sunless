using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestJournalUI : MonoBehaviour
{
	[SerializeField] GameObject questButtonPrefab;
	[SerializeField] GameObject questPartButtonPrefab;
	[SerializeField] GameObject activeTitlePrefab;
	[SerializeField] GameObject completedTitlePrefab;
	[SerializeField] GameObject leftPartContainer;
	[SerializeField] GameObject questPartContainer;

	List<GameObject> questButtons;
	List<GameObject> questPartButtons;
	public void SetQuestUI()
	{
		ClearUI();
		SetUI();
	}

	void ClearUI()
	{
		ClearQuestButtons(questButtons);
		ClearQuestButtons(questPartButtons);
	}

	void ClearQuestButtons(List<GameObject> buttons)
	{
		foreach(GameObject button in buttons)
			Destroy(button);
	}
	void SetUI()
	{
		SetLeftPanelDividing(activeTitlePrefab);
		SetAllQuestButtons(QuestManager.Instance.activeQuests);
		SetAllQuestButtons(QuestManager.Instance.completedQuests);
		SetLeftPanelDividing(completedTitlePrefab);
		
		//SetQuestPartsOnUI(0);
	}

	void SetLeftPanelDividing(GameObject gameObject)
	{
		//...
		//questbuttonAdd(res);
	}

	public void SetQuestPartsOnUI(int index)
	{
		ClearQuestButtons(questPartButtons);
		//...
	}

	void SetAllQuestButtons(List<Quest> quests) 
	{
		for (int i = quests.Count - 1; i >= 0; i--){
			//...
		}
	}

   public void OnCloseButtonClicked()
	{
		gameObject.SetActive(false);
	}
}
