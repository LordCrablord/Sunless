using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New QuestDatabase", menuName = "Quests/QuestDatabase")]
public class QuestDatabase : ScriptableObject
{
    public List<Quest> quests;
}
