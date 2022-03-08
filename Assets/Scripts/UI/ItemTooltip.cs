using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameTMP;
    [SerializeField] TextMeshProUGUI descriptionTMP;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI goldCost;
    [SerializeField] TextMeshProUGUI tagsTMP;

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
        tagsTMP.text = Enum.GetName(typeof(ItemType), item.itemType);
            
		switch (item)
		{
            case Weapon w:
                weaponObject.SetActive(true);
                damageTMP.text = w.damage.ToString();
                critChanceTMP.text = w.critChance + "%";
                critValueTMP.text = w.critValue * 100 + "%";
                tagsTMP.text += ", "+Enum.GetName(typeof(WeaponType), w.weaponType)
                    +", " + Enum.GetName(typeof(HandUsage), w.handUsage);
                break;
            case Armor a:
                armorObject.SetActive(true);
                armorTMP.text = a.armorValue.ToString();
                tagsTMP.text += ", " + Enum.GetName(typeof(ArmorWeight), a.armorWeight);
                break;
            default:
                Debug.LogError("Not yet implemented");
                break;
		}
	}
}
