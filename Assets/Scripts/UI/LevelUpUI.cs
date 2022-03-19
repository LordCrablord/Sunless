using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelUpUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelUpPointsTMP;
    [SerializeField] TextMeshProUGUI strTMP;
    [SerializeField] TextMeshProUGUI dexTMP;
    [SerializeField] TextMeshProUGUI conTMP;
    [SerializeField] TextMeshProUGUI intTMP;


    PlayerCharacterStats characterStats;

    public void SetLevelUpScreen(PlayerCharacterStats stats)
	{
        characterStats = stats;
        levelUpPointsTMP.text = characterStats.LevelUpPoints.ToString();
        strTMP.text = characterStats.Str.ToString();
        dexTMP.text = characterStats.Dex.ToString();
        conTMP.text = characterStats.Con.ToString();
        intTMP.text = characterStats.Int.ToString();
    }
}
