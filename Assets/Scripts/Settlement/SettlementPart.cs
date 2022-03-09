using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SettlementPart", menuName = "Settlement/SettlementPart")]
public class SettlementPart : ScriptableObject
{
    public string settlementPartName;
    public Sprite sprite;
}
