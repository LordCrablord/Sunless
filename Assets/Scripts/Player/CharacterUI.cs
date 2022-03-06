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

        maxHpTMP.text = characterStats.Hp.ToString() + "/" + characterStats.HpMax.ToString();
        healthSlider.maxValue = characterStats.HpMax;
        healthSlider.value = characterStats.Hp;
    }
       
    // Update is called once per frame
    void Update()
    {
        
    }
}
