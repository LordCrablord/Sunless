using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUIHolderCharDefault : AbilityUIHolderChar
{
	string UICanvasName = "CharacterCanvasUI";
	public void OnClick()
	{

		if (transform.parent.parent.gameObject.name.ToString() == UICanvasName)
		{
			transform.parent.parent.gameObject.GetComponent<CharacterUI>(
				).SetAbilityPicker(gameObject);
		}
		else
		{
			Debug.LogError(transform.parent.parent.gameObject.name.ToString() + " is not " + UICanvasName);
			return;
		}
	}
}
