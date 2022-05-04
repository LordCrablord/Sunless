using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUIHolderChar : MonoBehaviour
{
	protected Ability ability;
	protected int abilityPosition;
	[SerializeField] protected GameObject AbilityInfoPrefab;
	[SerializeField] Image image;
	protected GameObject abilityInfo;
	[SerializeField] protected Vector2 itemTooltipOffset = new Vector2(-800, 0);
	protected float tooltipWait = 0.25f;
	
	public virtual void SetAbilityHolder(Ability a, int position)
	{
		ability = a;
		abilityPosition = position;
		if(ability != null)
		{
			image.sprite = ability.sprite;
			SetImageTransparency(1);
		}
		else
		{
			image.sprite = null;
			SetImageTransparency(0);
		}
	}

	protected void SetImageTransparency(float transparency)
	{
		Color color = image.color;
		color.a = transparency;
		image.color = color;
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

	public Ability GetAbility()
	{
		return ability;
	}

	public int GetAbilityPosition()
	{
		return abilityPosition;
	}
}
