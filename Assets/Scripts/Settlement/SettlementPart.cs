using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SettlementPart", menuName = "Settlement/SettlementPart")]
public class SettlementPart : ScriptableObject
{
    public string settlementPartName;
    public int id;
    public Sprite sprite;
    public List<SettlementEvent> settlementEvents;
}

[Serializable]
public class SettlementEvent
{
    public string eventName;
    public Sprite sprite;
    public DialogueAction dialogueAction;
}

