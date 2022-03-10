using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New QuestPart", menuName = "Quests/QuestPart")]
public class QuestPart : ScriptableObject
{
    public int questPartID;
    public string partTitle;
    public string description;
}
