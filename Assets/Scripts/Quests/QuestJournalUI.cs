using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestJournalUI : MonoBehaviour
{
	[SerializeField] GameObject questButtonPrefab;
	[SerializeField] GameObject questPartButtonPrefab;
	[SerializeField] GameObject activeTitlePrefab;
	[SerializeField] GameObject completedTitlePrefab;

	[SerializeField] GameObject leftPartContainer;
	[SerializeField] GameObject questPartContainer;

	[SerializeField] TextMeshProUGUI questName;
	[SerializeField] TextMeshProUGUI questDescription;

	List<GameObject> questButtons = new List<GameObject>();
	List<GameObject> questPartButtons = new List<GameObject>();
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
		buttons.Clear();
	}
	void SetUI()
	{
		SetLeftPanelDividing(activeTitlePrefab);
		SetQuestButtons(QuestManager.Instance.activeQuests);
		SetLeftPanelDividing(completedTitlePrefab);
		SetQuestButtons(QuestManager.Instance.completedQuests);

		
		//SetQuestInfoOnUI(0);
	}

	void SetLeftPanelDividing(GameObject gameObject)
	{
		GameObject panelDivider = Instantiate(gameObject, transform.position, Quaternion.identity);
		panelDivider.transform.SetParent(leftPartContainer.transform, false);
		questButtons.Add(panelDivider);
	}

	void SetQuestButtons(List<Quest> quests) 
	{
		for (int i = quests.Count - 1; i >= 0; i--){
			GameObject questButton = Instantiate(questButtonPrefab, transform.position, Quaternion.identity);
			questButton.transform.SetParent(leftPartContainer.transform, false);
			questButton.GetComponent<QuestButtonUI>().SetQuestButton(this, quests[i]);
			questButtons.Add(questButton);
			if (quests[i] == QuestManager.Instance.currentlyFollowedQuest)
			{
				//markquestonUI;
			}
		}
	}

	public void SetQuestInfoOnUI(Quest quest)
	{
		questName.text = quest.questTitle;
		questDescription.text = quest.description;
		//...for questparts
		ClearQuestButtons(questPartButtons);
	}

   public void OnCloseButtonClicked()
	{
		gameObject.SetActive(false);
	}
}
