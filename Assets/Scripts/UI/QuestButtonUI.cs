using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestButtonUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questTitle;
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
        //...mark quest as followed if needed
        questJournal.SetQuestInfoOnUI(quest);
    }
}
