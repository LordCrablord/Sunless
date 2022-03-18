﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [Header("Health Bar")]
    [SerializeField] TextMeshProUGUI maxHpTMP;
    [SerializeField] Slider healthSlider;

    [Header("XP Bar & Level")]
    [SerializeField] TextMeshProUGUI xpTMP;
    [SerializeField] Slider xpSlider;
    [SerializeField] TextMeshProUGUI levelTMP;

    [Header("Equipment")]
    [SerializeField] GameObject helmetUIHolder;
    [SerializeField] GameObject chestpieceUIHolder;
    [SerializeField] GameObject weaponUIHolder;

    [Header("Damage & Crit")]
    [SerializeField] TextMeshProUGUI damageTMP;
    [SerializeField] TextMeshProUGUI critChanceTMP;
    [SerializeField] TextMeshProUGUI critValueTMP;

    [Header("Armor")]
    [SerializeField] TextMeshProUGUI protPierceTMP;
    [SerializeField] TextMeshProUGUI protSlashTMP;
    [SerializeField] TextMeshProUGUI protBludgeTMP;
    [SerializeField] TextMeshProUGUI protElementTMP;
    [SerializeField] TextMeshProUGUI protEldrichTMP;
    [SerializeField] TextMeshProUGUI protArcaneTMP;

    [Header("Ability Score")]
    [SerializeField] TextMeshProUGUI strTMP;
    [SerializeField] TextMeshProUGUI dexTMP;
    [SerializeField] TextMeshProUGUI conTMP;
    [SerializeField] TextMeshProUGUI intTMP;

    [Header("Other")]
    [SerializeField] TextMeshProUGUI nameTMP;
    [SerializeField] Image characterImage;
    [SerializeField] TextMeshProUGUI goldTMP;
    [SerializeField] GameObject inventoryItemPickerPrefab;
    [SerializeField] List<PartyMemberHolder> partyMembers;

    GameObject inventoryItemPicker;
    PlayerCharacterStats characterStats;


    public void SetCharacterUI(PlayerCharacterStats stats)
	{
        characterStats = stats;

        nameTMP.text = characterStats.characterName;
        characterImage.sprite = characterStats.sprite;
        goldTMP.text = characterStats.Gold.ToString();

        SetArmor();
        SetAbilityScores();
        SetHealthUI();
        SetXpUI();
        SetPartyMember();

        damageTMP.text = characterStats.DamageMin + " - " + characterStats.DamageMax;
        critChanceTMP.text = characterStats.CritChance + "%";
        critValueTMP.text = characterStats.CritValue * 100 + "%";

        helmetUIHolder.GetComponent<ItemUIHolder>().SetUIHolder(characterStats.helmet);
        chestpieceUIHolder.GetComponent<ItemUIHolder>().SetUIHolder(characterStats.chestpiece);
        weaponUIHolder.GetComponent<ItemUIHolder>().SetUIHolder(characterStats.weapon);

    }

    void SetArmor()
	{
        protPierceTMP.text = characterStats.ProtPierce.ToString();
        protSlashTMP.text = characterStats.ProtSlash.ToString();
        protBludgeTMP.text = characterStats.ProtBludge.ToString();
        protElementTMP.text = characterStats.ProtElement.ToString();
        protEldrichTMP.text = characterStats.ProtEldrich.ToString();
        protArcaneTMP.text = characterStats.ProtArcane.ToString();
    }

    void SetAbilityScores()
	{
        strTMP.text = characterStats.Str.ToString();
        dexTMP.text = characterStats.Dex.ToString();
        conTMP.text = characterStats.Con.ToString();
        intTMP.text = characterStats.Int.ToString();
    }
    void SetHealthUI()
	{
        maxHpTMP.text = characterStats.Hp.ToString() + "/" + characterStats.HpMax.ToString();
        healthSlider.maxValue = characterStats.HpMax;
        healthSlider.value = characterStats.Hp;
    }

    void SetXpUI()
    {
        levelTMP.text = characterStats.Level.ToString();
        xpTMP.text = characterStats.Xp.ToString() + "/" + characterStats.levelXpThreshold[characterStats.Level];
        xpSlider.maxValue = characterStats.levelXpThreshold[characterStats.Level];
        xpSlider.minValue = characterStats.levelXpThreshold[characterStats.Level - 1];
        xpSlider.value = characterStats.Xp;
    }

    void SetPartyMember()
	{
        PlayerCharacterStats[] stats = GameManager.Instance.GetPlayerParty();
        for(int i=0; i < stats.Length; i++)
		{
            partyMembers[i].SetPartyMemberHolder(stats[i], i);
		}
	}

    public void SetInventoryItemPicker(GameObject itemUIHolder)
	{
        if (inventoryItemPicker != null)
        {
            Destroy(inventoryItemPicker);
        }

        inventoryItemPicker = Instantiate(inventoryItemPickerPrefab, inventoryItemPickerPrefab.transform.position, Quaternion.identity);
        inventoryItemPicker.transform.SetParent(gameObject.transform, false);

        RectTransform inventoryRect = inventoryItemPicker.GetComponent<RectTransform>();
        RectTransform itemUIRect = itemUIHolder.GetComponent<RectTransform>();

        inventoryRect.anchoredPosition = new Vector2(
            itemUIRect.anchoredPosition.x, inventoryRect.anchoredPosition.y) + inventoryItemPicker.GetComponent<InventoryItemPicker>().inventoryOffset;

        inventoryItemPicker.GetComponent<InventoryItemPicker>().SetItems(itemUIHolder.GetComponent<ItemUIHolder>(), characterStats.InventoryBack);
    }

    public void SwapItems(Item oldItem, Item newItem)
	{
        characterStats.UnequipItem(oldItem);
        characterStats.EquipItem(newItem);
        SetCharacterUI(characterStats);
	}
}
