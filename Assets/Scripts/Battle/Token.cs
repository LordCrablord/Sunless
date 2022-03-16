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
    [SerializeField] Color highlightedColor;
    [SerializeField] Color defaultSelectedColor;

    [SerializeField] GameObject damageAnimPrefab;

    CharacterStats stat;
    bool isSelected;
    bool isSubscribed = false;

    float randDelayMod = 1f;
	private void Start()
    { 
        float rand = Random.Range(0, randDelayMod);
        StartCoroutine(StartAnim(rand));
    }

    IEnumerator StartAnim(float val)
	{
        yield return new WaitForSeconds(val);
        gameObject.GetComponent<Animator>().Play("Token Static");
    }

	public void SetToken(CharacterStats newStats, int pos)
	{
        stat = newStats;
        stat.Position = pos;
        nameTMP.text = stat.characterName;
        image.sprite = stat.sprite;
        SetHealthUI();

		if (!isSubscribed)
		{
            stat.HealthChanged += SetHealthUI;
            stat.TurnStarted += OnTurnStartedUI;
            stat.TurnEnded += OnTurnEndedUI;
            stat.Damaged += DoCharacterDamageAnimation;
            stat.Killed += DestroyToken;
            stat.ActionStarted += ShowWhatActiondone;
            isSubscribed = true;
        }
       
    }

    public CharacterStats GetStat()
	{
        return stat;
	}

    void OnTurnStartedUI()
	{
        Debug.Log(stat.characterName + " has started his turn!");
        gameObject.GetComponent<Animator>().Play("Token New Turn");
    }

    void OnTurnEndedUI()
    {
        gameObject.GetComponent<Animator>().Play("Token Static");
    }

    void DoCharacterDamageAnimation(object sender, DamageEventArgs e)
	{
        Animator temp = GetComponent<Animator>();

        gameObject.GetComponent<Animator>().Play("Token Damage Recoil");
        GameObject obj = Instantiate(damageAnimPrefab, transform.position, Quaternion.identity);
        obj.transform.SetParent(gameObject.transform, false);
        obj.GetComponent<DamageAnimation>().StartAnimation(e);
	}

    void ShowWhatActiondone(object sender, string actionName)
	{
        GameObject obj = Instantiate(damageAnimPrefab, transform.position, Quaternion.identity);
        obj.transform.SetParent(gameObject.transform, false);
        obj.GetComponent<DamageAnimation>().StartAnimation(actionName);
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
        selectImage.color = highlightedColor;
    }

	public void OnMouseExit()
	{
        nameTMP.gameObject.SetActive(false);
        selectImage.color = defaultSelectedColor;
    }

    public void SetSelectedStatus(bool selected)
	{
        isSelected = selected;
        selectImage.gameObject.SetActive(isSelected);
        selectImage.color = defaultSelectedColor;
	}

    public void OnClick()
	{
		if (isSelected)
		{
            BattleManager.Instance.AttemptAbilityAction(BattleManager.Instance.CurrentCharacter, BattleManager.Instance.selectedAbility, stat);
		}
	}

	public void DestroyToken()
	{
        stat.HealthChanged -= SetHealthUI;
        stat.TurnStarted -= OnTurnStartedUI;
        stat.TurnEnded -= OnTurnEndedUI;
        stat.Damaged -= DoCharacterDamageAnimation;
        stat.Killed -= DestroyToken;
        stat.ActionStarted -= ShowWhatActiondone;
        gameObject.GetComponent<Animator>().Play("Token Death");
        
	}

    public void OnDestroyAnimComplete()
	{
        Destroy(gameObject);
    }
}
