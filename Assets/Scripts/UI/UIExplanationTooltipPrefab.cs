using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIExplanationTooltipPrefab : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI tooltipText;
    public void SetTooltiptext(string s)
	{
		tooltipText.text = s;
	}
}
