using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Quest", menuName = "Quests/Quest")]
public class Quest : ScriptableObject
{
    public int questID;
    public string questTitle;
    public string description;
    public List<QuestPart> questParts;
    public QuestReward questReward;
}

[System.Serializable]
public class QuestReward
{
    public int gold;
    public int xp;
    public List<Item> items;
}
