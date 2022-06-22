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
    [SerializeField] QuestNotification questNotificationUI;
    bool questUIActive = false;

    public List<Quest> activeQuests;
    public List<Quest> completedQuests;

    public event Notify CurrentlyFollowedQuestChanged;

    [SerializeField] Quest currentlyFollowedQuest;
    public Quest CurrentlyFollowedQuest 
    { 
        get { return currentlyFollowedQuest; } 
        set 
        { 
            currentlyFollowedQuest = value;
            OnCurrentlyFollowedQuestChanged();
        }
    }

    protected virtual void OnCurrentlyFollowedQuestChanged()
	{
        CurrentlyFollowedQuestChanged?.Invoke();
    }

    public void AddToActiveQuests(int id)
	{
        Quest quest = questDatabase.quests.Find(q=>q.questID == id);
        activeQuests.Add(quest);
        CurrentlyFollowedQuest = quest;
        triggerManager.AddToQuestPartAllowList(quest.questParts[0].questPartID);
        questNotificationUI.PlayNotification("Quest started: " + quest.questTitle, quest.questParts[0].partTitle);
	}

    public void NotifyPlayerOnQuestPart(int partID)
    {
        Quest quest = questDatabase.quests.Where(q => q.questParts.Where(p=>p.questPartID==partID).First()).First();
        QuestPart questPart = quest.questParts.Where(p => p.questPartID == partID).First();
        questNotificationUI.PlayNotification("Quest updated: " + quest.questTitle, questPart.partTitle);
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

    public void CompleteQuest(int id)
	{
        Quest quest = activeQuests.Find(q => q.questID == id);
        if(quest == null)
		{
            Debug.LogError("Error, can't find active quest with index " + id);
            return;
		}
        questNotificationUI.PlayNotification("Quest completed: " + quest.questTitle, "+" + quest.questReward.xp + " XP");
        activeQuests.Remove(quest);
        completedQuests.Add(quest);

        PlayerCharacterStats player = GameManager.Instance.GetMainCharacter();
        player.Gold += quest.questReward.gold;
        player.Xp += quest.questReward.xp;
        foreach(Item item in quest.questReward.items)
		{
            player.InventoryBack.Add(item);
		}
        
        if(CurrentlyFollowedQuest == quest)
		{
            OnCurrentlyFollowedQuestChanged();
            CurrentlyFollowedQuest = null;
        }
            
    }
}
