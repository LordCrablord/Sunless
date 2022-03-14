using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponAbilityUIHolder : MonoBehaviour
{
    WeaponAttackAbility attackAbility;
    [SerializeField] Image image;
	[SerializeField] GameObject panelBlocker;
    [SerializeField] GameObject AbilityInfoPrefab;
    GameObject abilityInfo;
	[SerializeField] protected Vector2 itemTooltipOffset = new Vector2(-800, 0);
	float tooltipWait = 0.25f;

	public void SetWeaponAbilityUI(WeaponAttackAbility ab, int position)
	{
        attackAbility = ab;
        image.sprite = attackAbility.sprite;
		TargetPosition charPos = (TargetPosition)position;
		if (attackAbility.allowedFromPosition.Contains(charPos))
		{
			GetComponent<Button>().interactable = true;
			panelBlocker.SetActive(false);
		}
		else
		{
			GetComponent<Button>().interactable = false;
			panelBlocker.SetActive(true);
		}
		if (ab.apCost > BattleManager.Instance.CurrentCharacter.Ap)
		{
			GetComponent<Button>().interactable = false;
			panelBlocker.SetActive(true);
		}
	}

	public void ClearWeaponAbilityUI()
	{
		attackAbility = null;
		image.sprite = null;
		GetComponent<Button>().interactable = false;
		panelBlocker.SetActive(true);
	}

	public void OnAbilityCliked()
	{
		if(attackAbility != null)
		{
			BattleManager.Instance.AbilitySelect(attackAbility);
		}
	}

	public void OnMouseEnter()
	{
		if (attackAbility != null)
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
		abilityInfo.GetComponent<AbilityInfo>().SetAbilityTooltip(attackAbility);
	}
}
