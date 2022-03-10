using System.Collections;
using System.Collections.Generic;
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


    public void ToggleQuestJournalUI()
    {
        questUIActive = !questUIActive;
        questUI.SetActive(questUIActive);
        //if (charatcerUIActive) SetCharacterDataOnUI(stats);
    }
}
