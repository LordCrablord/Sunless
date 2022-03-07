using System.Collections;
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

    [Header("Other")]
    [SerializeField] TextMeshProUGUI nameTMP;
    [SerializeField] Image characterImage;
    [SerializeField] TextMeshProUGUI goldTMP;


    PlayerCharacterStats characterStats;


    public void SetCharacterUI(PlayerCharacterStats stats)
	{
        characterStats = stats;

        nameTMP.text = characterStats.characterName;
        characterImage.sprite = characterStats.sprite;
        goldTMP.text = characterStats.Gold.ToString();
        SetHealthUI();
        SetXpUI();

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
