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
        stat.HealthChanged += SetHealthUI;
        stat.TurnStarted += OnTurnStartedUI;
        nameTMP.text = stat.characterName;
        image.sprite = stat.sprite;
        SetHealthUI();
    }

    void OnTurnStartedUI()
	{
        Debug.Log(stat.characterName + " has started his turn!");
	}

	void SetHealthUI()
	{
        hpTMP.text = stat.Hp.ToString() + "/" + stat.HpMax.ToString();
        healthSlider.maxValue = stat.HpMax;
        healthSlider.value = stat.Hp;
    }

	public void OnMouseEnter()
	{
        nameTMP.gameObject.SetActive(true);
    }

	public void OnMouseExit()
	{
        nameTMP.gameObject.SetActive(false);
    }

	public void DestroyToken()
	{
        stat.HealthChanged -= SetHealthUI;
        stat.TurnStarted -= OnTurnStartedUI;
        Destroy(gameObject);
	}
}
