using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameTMP;
    [SerializeField] TextMeshProUGUI descriptionTMP;

    public void SetTooltip(Item item)
	{
        nameTMP.text = item.itemName;
        descriptionTMP.text = item.description;
	}
}
