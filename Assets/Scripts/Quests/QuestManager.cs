using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    [SerializeField] EventTriggerManager triggerManager;
    public EventTriggerManager TriggerManager { get { return triggerManager; } }

    [SerializeField] QuestDatabase questDatabase;
    [SerializeField] GameObject questUI;
    bool questUIActive = false;

    public List<Quest> activeQuests;
    public List<Quest> completedQuests;
    public Quest currentlyFollowedQuest;

    public void AddToActiveQuests(int id)
	{
        Quest quest = questDatabase.quests.Find(q=>q.questID == id);
        activeQuests.Add(quest);
        currentlyFollowedQuest = quest;
        triggerManager.AddToQuestPartAllowList(quest.questParts[0].questPartID);
	}

    public void ToggleQuestJournalUI()
    {
        questUIActive = !questUIActive;
        questUI.SetActive(questUIActive);
        if (questUIActive) questUI.GetComponent<QuestJournalUI>().SetQuestUI();
    }

    public List<QuestPart> GetAllowedQuestParts(Quest quest)
	{
        return quest.questParts.Where(p => TriggerManager.questPartAllowID.Contains(p.questPartID)).ToList();
	}
}
