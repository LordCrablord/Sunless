using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Token : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameTMP;
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI hpTMP;
    [SerializeField] Slider healthSlider;

    CharacterStats stat;

    public void SetToken(CharacterStats newStats)
	{
        stat = newStats;
        nameTMP.text = stat.characterName;
        image.sprite = stat.sprite;
        hpTMP.text = stat.Hp.ToString() + "/" + stat.HpMax.ToString();
        healthSlider.value = stat.Hp;
        healthSlider.maxValue = stat.HpMax;
    }
}
