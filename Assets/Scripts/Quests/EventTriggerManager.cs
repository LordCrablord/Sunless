using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void Notify();
public class EventTriggerManager : MonoBehaviour
{
    public List<int> settlementConditionForbidID = new List<int>();
    public List<int> settlementConditionAllowID = new List<int>();

    public event Notify SettlementTriggerListsModified;
    protected virtual void OnSettlementTriggerListsModified()
    {
        SettlementTriggerListsModified?.Invoke();
    }
}
