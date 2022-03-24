using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelUpPointsTMP;
    [SerializeField] TextMeshProUGUI abilityToLearnTMP;
    [SerializeField] TextMeshProUGUI strTMP;
    [SerializeField] TextMeshProUGUI dexTMP;
    [SerializeField] TextMeshProUGUI conTMP;
    [SerializeField] TextMeshProUGUI intTMP;

    [SerializeField] TextMeshProUGUI abilityPoints;
    [SerializeField] AbilityPickerLevelUp abilityLevelUp;


    PlayerCharacterStats characterStats;
    int levelUpPoints;
    int abilityToLearn;
    List<Ability> newAbilities = new List<Ability>();

    public void SetLevelUpScreen(PlayerCharacterStats stats)
	{
        characterStats = stats;
        levelUpPoints = characterStats.LevelUpPoints;
        levelUpPointsTMP.text = characterStats.LevelUpPoints.ToString();
        abilityToLearn = characterStats.AbilityToLearn;
        abilityToLearnTMP.text = abilityToLearn.ToString();
        newAbilities.Clear();

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

    public void ToggleAbilityPick(GameObject originator, Ability ability)
	{
		if (newAbilities.Contains(ability))
		{
            abilityToLearn++;
            abilityToLearnTMP.text = abilityToLearn.ToString();
            newAbilities.Remove(ability);
            abilityLevelUp.SetActiveColorForAbility(originator, false);
		}
		else
		{
            if (abilityToLearn <= 0) return;

            abilityToLearn--;
            abilityToLearnTMP.text = abilityToLearn.ToString();
            newAbilities.Add(ability);
            abilityLevelUp.SetActiveColorForAbility(originator, true);
        }
		
	}

    public bool CanTakeAbilityOnLevelUpCheck(SpellAbility ability)
	{
        foreach(ConditionValues condition in ability.levelUpConditions)
		{
            if ((float)characterStats.StatsDictionary[condition.stat].Get() < condition.addVal)
                return false;
		}
        return true;
	}

    public void OnAcceptClick()
	{
        characterStats.Str += int.Parse(strTMP.text) - characterStats.Str;
        characterStats.Dex += int.Parse(dexTMP.text) - characterStats.Dex;
        characterStats.Con += int.Parse(conTMP.text) - characterStats.Con;
        characterStats.Int += int.Parse(intTMP.text) - characterStats.Int;
        characterStats.LevelUpPoints = levelUpPoints;

        characterStats.AbilityToLearn = abilityToLearn;
        foreach (Ability ability in newAbilities)
            characterStats.KnownAbilities.Add((SpellAbility)ability);

        gameObject.SetActive(false);
        GameManager.Instance.SetCharacterDataOnUI(characterStats);
    }
}
