﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityUIHolder : WeaponAbilityUIHolder
{
	public void SetAbilityUI(Ability ab, int position)
	{
		ability = ab;
		if(ability != null)
		{
			image.sprite = ability.sprite;
			SetImageTransparency(1);
			SetUIHolder(position);
		}
		else
		{
			image.sprite = null;
			SetImageTransparency(0);
			GetComponent<Button>().interactable = true;
			panelBlocker.SetActive(false);
		}
	}

	void SetImageTransparency(float transparency)
	{
		Color color = image.color;
		color.a = transparency;
		image.color = color;
	}
}