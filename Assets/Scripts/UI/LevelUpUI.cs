using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelUpPointsTMP;
    [SerializeField] TextMeshProUGUI strTMP;
    [SerializeField] TextMeshProUGUI dexTMP;
    [SerializeField] TextMeshProUGUI conTMP;
    [SerializeField] TextMeshProUGUI intTMP;

    [SerializeField] TextMeshProUGUI abilityPoints;
    [SerializeField] AbilityPickerLevelUp abilityLevelUp;


    PlayerCharacterStats characterStats;
    int levelUpPoints;

    public void SetLevelUpScreen(PlayerCharacterStats stats)
	{
        characterStats = stats;
        levelUpPoints = characterStats.LevelUpPoints;
        levelUpPointsTMP.text = characterStats.LevelUpPoints.ToString();

        strTMP.text = characterStats.Str.ToString();
        dexTMP.text = characterStats.Dex.ToString();
        conTMP.text = characterStats.Con.ToString();
        intTMP.text = characterStats.Int.ToString();

        abilityLevelUp.SetAbilitiesInPicker(stats);
    }

    public void IncreaseValue(TextMeshProUGUI statTMP)
	{
		if (levelUpPoints > 0)
		{
            int stat = Int32.Parse(statTMP.text);
            stat++;
            statTMP.text = stat.ToString();
            levelUpPoints--;
            levelUpPointsTMP.text = (Int32.Parse(levelUpPointsTMP.text) - 1).ToString();
        } 
	}

    public void OnReset()
	{
        SetLevelUpScreen(characterStats);
    }

    public void OnAcceptClick()
	{
        characterStats.Str += int.Parse(strTMP.text) - characterStats.Str;
        characterStats.Dex += int.Parse(dexTMP.text) - characterStats.Dex;
        characterStats.Con += int.Parse(conTMP.text) - characterStats.Con;
        characterStats.Int += int.Parse(intTMP.text) - characterStats.Int;
        characterStats.LevelUpPoints = levelUpPoints;

        //TODO Ability pick

        gameObject.SetActive(false);
        GameManager.Instance.SetCharacterDataOnUI(characterStats);
    }
}
