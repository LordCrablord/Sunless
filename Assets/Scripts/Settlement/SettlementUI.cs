using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettlementUI : MonoBehaviour
{
    public GameObject titleTMP;
    SettlementData settlementData;

    
    public void SetUI(SettlementData data)
	{

	}

    public void SetTitle(string title)
	{
        titleTMP.GetComponent<TextMeshProUGUI>().text = title;
	}

    public void CloseUI()
	{
        Destroy(gameObject);
	}

}
