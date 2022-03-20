using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New AbilityDB", menuName = "Ability/AbilityDatabase")]
public class AbilityDatabase : ScriptableObject
{
    public List<SpellAbility> abilities;
}
