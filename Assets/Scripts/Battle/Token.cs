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
    [SerializeField] Image selectImage;

    CharacterStats stat;
    bool isSelected;

    public void SetToken(CharacterStats newStats, int pos)
	{
        stat = newStats;
        stat.Position = pos;
        stat.HealthChanged += SetHealthUI;
        stat.TurnStarted += OnTurnStartedUI;
        nameTMP.text = stat.characterName;
        image.sprite = stat.sprite;
        SetHealthUI();
    }

    public CharacterStats GetStat()
	{
        return stat;
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

    public void SetSelectedStatus(bool selected)
	{
        isSelected = selected;
        selectImage.gameObject.SetActive(isSelected);
	}

    public void OnClick()
	{
        Debug.Log("Clicked on " + stat.characterName);
	}

	public void DestroyToken()
	{
        stat.HealthChanged -= SetHealthUI;
        stat.TurnStarted -= OnTurnStartedUI;
        Destroy(gameObject);
	}
}
