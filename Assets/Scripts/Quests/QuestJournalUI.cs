using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;

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

	public Quest selectedQuest;
	public GameObject selectedQuestButton;
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
			if (quests[i] == QuestManager.Instance.CurrentlyFollowedQuest)
			{
				questButton.GetComponent<QuestButtonUI>().OnQuestButtonClicked();
				questButton.GetComponent<QuestButtonUI>().ToggleQuestMark(true);
				selectedQuestButton = questButton;
			}
		}
	}

	public void SetQuestInfoOnUI(Quest quest)
	{
		selectedQuest = quest;
		questName.text = quest.questTitle;
		questDescription.text = quest.description;

		ClearQuestButtons(questPartButtons);
		List<QuestPart> questParts = QuestManager.Instance.GetAllowedQuestParts(quest);
		for(int i = questParts.Count-1; i>=0; i--)
		{
			GameObject questButton = Instantiate(questPartButtonPrefab, transform.position, Quaternion.identity);
			questButton.transform.SetParent(questPartContainer.transform, false);
			questButton.GetComponent<QuestPartUI>().SetQuestPartUI(questParts[i]);
			questPartButtons.Add(questButton);
		}
	}

   public void OnCloseButtonClicked()
	{
		QuestManager.Instance.ToggleQuestJournalUI();
	}
}
