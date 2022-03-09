using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//here once was CHECK_IF_FORBIDDEN
public enum TriggerCondition { NONE, CHECK_IF_ALLOWED }
[CreateAssetMenu(fileName = "New SettlementPart", menuName = "Settlement/SettlementPart")]
public class SettlementPart : ScriptableObject
{
    public string settlementPartName;
    public int id;
    public TriggerCondition triggerCondition;
    public Sprite sprite;
    public List<SettlementEvent> settlementEvents;
}


[Serializable]
public class SettlementEvent
{
    public string eventName;
    public int eventID;
    public TriggerCondition triggerCondition;
    public Sprite sprite;
    public DialogueAction dialogueAction;
}

