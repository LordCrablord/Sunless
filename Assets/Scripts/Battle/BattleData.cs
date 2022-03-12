using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New BattleData", menuName = "Battle/BattleData")]
public class BattleData : ScriptableObject
{
    public int battleID;
    public List<NonPlayerCharacterStats> enemies;
}
