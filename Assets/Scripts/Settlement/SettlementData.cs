using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New SettlementData", menuName = "Settlement/SettlementData")]
public class SettlementData : ScriptableObject
{
    public string settlementName;
    public List<int> temp;
}
