using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Notify();
public class EventTriggerManager : MonoBehaviour
{
    public List<int> settlementConditionForbidID = new List<int>();
    
    public List<int> questPartAllowID = new List<int>();

    public Dictionary<int, Vector3> markPositions = new Dictionary<int, Vector3>();

    public event Notify SettlementTriggerListsModified;
    public event Notify QuestPartAllowed;
    protected virtual void OnSettlementTriggerListsModified()
    {
        SettlementTriggerListsModified?.Invoke();
    }

    protected virtual void OnQuestPartAllowed()
    {
        QuestPartAllowed?.Invoke();
    }

    public void AddToConditionForbidList(int triggerID)
	{
        settlementConditionForbidID.Add(triggerID);
        OnSettlementTriggerListsModified();
    }
    public void RemoveFromConditionForbidList(int triggerID)
    {
        settlementConditionForbidID.Remove(triggerID);
        OnSettlementTriggerListsModified();
    }

    public void AddToQuestPartAllowList(int triggerID)
	{
        questPartAllowID.Add(triggerID);
        OnQuestPartAllowed();
        QuestManager.Instance.NotifyPlayerOnQuestPart(triggerID);
    }

    public void RemoveFromQuestPartAllowList(int triggerID)
    {
        questPartAllowID.Remove(triggerID);
    }
}
