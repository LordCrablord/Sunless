using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI maxHpTMP;
    [SerializeField] Slider healthSlider; 
    CharacterStats characterStats;


    public void SetCharacterUI(CharacterStats stats)
	{
        characterStats = stats;


        SetHealthUI();
	}

    void SetHealthUI()
	{
        float tempVal = 10;

        maxHpTMP.text = tempVal.ToString() + "/" + characterStats.HpMax.ToString();
        healthSlider.maxValue = characterStats.HpMax;
        healthSlider.value = tempVal;
    }
       
    // Update is called once per frame
    void Update()
    {
        
    }
}
