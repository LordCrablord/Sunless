using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PartyMemberHolder : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [SerializeField] Image characterSprite;
    PlayerCharacterStats characterStats;

	static PlayerCharacterStats dragDestinationStats;
	public void SetPartyMemberHolder(PlayerCharacterStats stats)
	{
        characterStats = stats;
        if(characterStats != null)
		{
			SetImageTransparency(1);
            characterSprite.sprite = characterStats.characterIconUI;
		}
		else
		{
			SetImageTransparency(0);
		}
        
	}

	void SetImageTransparency(float transparency)
	{
		Color color = characterSprite.color;
		color.a = transparency;
		characterSprite.color = color;
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		Debug.Log("Drag started!");
	}

	public void OnDrag(PointerEventData eventData)
	{
		characterSprite.GetComponent<RectTransform>().position = eventData.position;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		characterSprite.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
	}

	public void OnPoinerEnter()
	{
		dragDestinationStats = characterStats;
	}

	public void OnPointerExit()
	{
		dragDestinationStats = null;
	}
}
