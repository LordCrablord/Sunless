using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestButtonUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questTitle;
    [SerializeField] GameObject followQuestMarker;
    Quest quest;
    QuestJournalUI questJournal;

    public void SetQuestButton(QuestJournalUI journal, Quest q)
	{
        questJournal = journal;
        quest = q;
        questTitle.text = quest.questTitle;
	}

    public void OnQuestButtonClicked()
	{
        if(quest == questJournal.selectedQuest)
		{
			if (questJournal.selectedQuestButton != null)
			{
                questJournal.selectedQuestButton.GetComponent<QuestButtonUI>().ToggleQuestMark(false);
            }
                
            ToggleQuestMark(true);
            questJournal.selectedQuestButton = this.gameObject;
        }
        questJournal.SetQuestInfoOnUI(quest);
    }

    public void ToggleQuestMark(bool activate)
	{
		if (activate)
		{
            QuestManager.Instance.currentlyFollowedQuest = quest;
		}
		else
		{
            QuestManager.Instance.currentlyFollowedQuest = null;
        }
        followQuestMarker.SetActive(activate);


    }

}
