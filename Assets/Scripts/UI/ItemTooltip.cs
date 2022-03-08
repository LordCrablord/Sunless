using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameTMP;
    [SerializeField] TextMeshProUGUI descriptionTMP;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI goldCost;

    [Header("Weapon")]
    [SerializeField] GameObject weaponObject;
    [SerializeField] TextMeshProUGUI damageTMP;
    [SerializeField] TextMeshProUGUI critChanceTMP;
    [SerializeField] TextMeshProUGUI critValueTMP;

    [Header("Armor")]
    [SerializeField] GameObject armorObject;
    [SerializeField] TextMeshProUGUI armorTMP;
    public void SetTooltip(Item item)
	{
        nameTMP.text = item.itemName;
        descriptionTMP.text = item.description;
        image.sprite = item.sprite;
        goldCost.text = item.goldCost + "g.";
	}
}
