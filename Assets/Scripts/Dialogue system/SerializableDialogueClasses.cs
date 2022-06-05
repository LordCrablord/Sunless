using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region DialogueSimple
[Serializable]
public class DialogueData
{
    public List<DialogueDataItem> items;
}
[Serializable]
public class DialogueDataItem
{
    public int id;
    public List<DialogueDataItemString> dialogue;
    public List<DialogueAction> actions;
}
[Serializable]
public class DialogueDataItemString
{
    public string name;
    public string imagePath;
    public string text;
}
[Serializable]
public class DialogueAction
{
    public int action_type;
    public int action_id;
}
#endregion

#region DialogueBranches
[Serializable]
public class DialogueBranchesData
{
    public List<DialogueBranchesDataItem> items;
}

[Serializable]
public class DialogueBranchesDataItem
{
    public int id;
    public List<DialogueDataItemString> dialogue;
    public List<DialogueBranch> branches;
}

[Serializable]
public class DialogueBranch
{
    public int branch_id;
    public string text;
    public List<BranchCondition> conditions;
    public List<DialogueAction> actions;
}

public enum BranchConditionType { QUEST_PRESENT, QUEST_NOT_PRESENT, 
    EVENT_PRESENT, EVENT_NOT_PRESENT, STR, DEX, CON, INT,
    IS_IN_PARTY, IS_NOT_IN_PARTY, HAS_FREE_PARTY_CELL, HAS_NO_FREE_PARTY_CELL, GOLD_REQUIRED}
[Serializable]
public class BranchCondition
{
    public BranchConditionType cond_type;
    public int cond_val;
}

#endregion

#region DialogueRandom
[Serializable]
public class DialogueRandomData
{
    public List<DialogueRandomDataItem> items;
}

[Serializable]
public class DialogueRandomDataItem
{
    public int id;
    public List<DialogueAction> random_options;
}
#endregion
#region DialogueStatChange
public class DialogueStatChangeData
{
    public List<DialogueStatChangeDataItem> items;
}

[Serializable]
public class DialogueStatChangeDataItem
{
    public int id;
    public string stat;
    public float value;
}
#endregion