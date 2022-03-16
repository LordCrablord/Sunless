using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponAbilityUIHolder : MonoBehaviour
{
	Ability ability;
    [SerializeField] Image image;
	[SerializeField] GameObject panelBlocker;
    [SerializeField] GameObject AbilityInfoPrefab;
    GameObject abilityInfo;
	[SerializeField] protected Vector2 itemTooltipOffset = new Vector2(-800, 0);
	float tooltipWait = 0.25f;

	public void SetWeaponAbilityUI(Ability ab, int position)
	{
		ability = ab;
        image.sprite = ability.sprite;
		SetUIHolder(position);
	}

	protected void SetUIHolder(int position)
	{
		TargetPosition charPos = (TargetPosition)position;
		if (ability.allowedFromPosition.Contains(charPos))
		{
			GetComponent<Button>().interactable = true;
			panelBlocker.SetActive(false);
		}
		else
		{
			GetComponent<Button>().interactable = false;
			panelBlocker.SetActive(true);
		}
		if (ability.apCost > BattleManager.Instance.CurrentCharacter.Ap)
		{
			GetComponent<Button>().interactable = false;
			panelBlocker.SetActive(true);
		}
	}

	public void ClearWeaponAbilityUI()
	{
		ability = null;
		image.sprite = null;
		GetComponent<Button>().interactable = false;
		panelBlocker.SetActive(true);
	}

	public void OnAbilityCliked()
	{
		if(ability != null)
		{
			BattleManager.Instance.AbilitySelect(ability);
		}
	}

	public void OnMouseEnter()
	{
		if (ability != null)
			StartCoroutine("ShowAbilityInfo");
	}

	public void OnMouseExit()
	{
		StopCoroutine("ShowAbilityInfo");
		if (abilityInfo != null)
			Destroy(abilityInfo);
	}

	protected virtual IEnumerator ShowAbilityInfo()
	{
		yield return new WaitForSeconds(tooltipWait);
		abilityInfo = Instantiate(AbilityInfoPrefab, transform.position, Quaternion.identity);
		abilityInfo.transform.SetParent(transform.parent.parent, false);
		abilityInfo.GetComponent<RectTransform>().anchoredPosition = itemTooltipOffset + gameObject.GetComponent<RectTransform>().anchoredPosition;
		abilityInfo.GetComponent<AbilityInfo>().SetAbilityTooltip(ability);
	}
}
