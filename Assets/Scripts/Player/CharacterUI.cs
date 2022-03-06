using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI maxHpTMP;
    [SerializeField] Slider healthSlider;
    [SerializeField] TextMeshProUGUI nameTMP;
    PlayerCharacterStats characterStats;


    public void SetCharacterUI(PlayerCharacterStats stats)
	{
        characterStats = stats;

        nameTMP.text = characterStats.characterName;
        SetHealthUI();
	}

    void SetHealthUI()
	{

        maxHpTMP.text = characterStats.Hp.ToString() + "/" + characterStats.HpMax.ToString();
        healthSlider.maxValue = characterStats.HpMax;
        healthSlider.value = characterStats.Hp;
    }
       
    // Update is called once per frame
    void Update()
    {
        
    }
}
