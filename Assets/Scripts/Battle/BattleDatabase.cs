using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New BattleDatabase", menuName = "Battle/BattleDatabase")]
public class BattleDatabase : ScriptableObject
{
    public List<BattleData> battles;
}
