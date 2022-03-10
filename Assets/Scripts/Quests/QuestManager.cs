using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : Singleton<QuestManager>
{
    [SerializeField] EventTriggerManager triggerManager;
    public EventTriggerManager TriggerManager { get { return triggerManager; } }

    [SerializeField] QuestDatabase questDatabase;

    public List<Quest> activeQuests;
    public List<Quest> completedQuests;
}
