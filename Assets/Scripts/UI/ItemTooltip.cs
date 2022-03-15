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
    [SerializeField] TextMeshProUGUI protPierceTMP;
    [SerializeField] TextMeshProUGUI protSlashTMP;
    [SerializeField] TextMeshProUGUI protBludgeTMP;
    [SerializeField] TextMeshProUGUI protElementTMP;
    [SerializeField] TextMeshProUGUI protEldrichTMP;
    [SerializeField] TextMeshProUGUI protArcaneTMP;

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
                damageTMP.text = w.minDamage + " - " + w.maxDamage;
                critChanceTMP.text = w.critChance + "%";
                critValueTMP.text = w.critValue * 100 + "%";
                tagsTMP.text += ", "+Enum.GetName(typeof(WeaponType), w.weaponType)
                    +", " + Enum.GetName(typeof(HandUsage), w.handUsage);
                break;
            case Armor a:
                armorObject.SetActive(true);
                SetArmor(a);
                tagsTMP.text += ", " + Enum.GetName(typeof(ArmorWeight), a.armorWeight);
                break;
            default:
                Debug.LogError("Not yet implemented");
                break;
		}
	}

    void SetArmor(Armor armor)
	{
        protPierceTMP.text = armor.GetArmorProtVal(Stats.PROT_PIERCE).ToString();
        protSlashTMP.text = armor.GetArmorProtVal(Stats.PROT_SLASH).ToString();
        protBludgeTMP.text = armor.GetArmorProtVal(Stats.PROT_BLUDGE).ToString();
        protElementTMP.text = armor.GetArmorProtVal(Stats.PROT_ELEMENT).ToString();
        protEldrichTMP.text = armor.GetArmorProtVal(Stats.PROT_ELDRICH).ToString();
        protArcaneTMP.text = armor.GetArmorProtVal(Stats.PROT_ARCANE).ToString();
    }
}
