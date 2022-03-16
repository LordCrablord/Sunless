using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Condition", menuName = "Ability/AbilityCondition")]
public class AbilityCondition:ScriptableObject
{
    public string conditionName;
    public int conditionID;
    public Sprite sprite;
    public float baseSaveDC;
    public List<Scaling> saveDCScaling;
    public float duration;
    public List<ConditionValues> conditionValues;
}

[System.Serializable]
public class Scaling
{
    public Stats stat;
    public float statMod;
}

[System.Serializable]
public class ConditionValues
{
    public Stats stat;
    public float addVal;
    public float multVal;
    public bool hasBaseValue = false;
    public Stats baseValue;
}