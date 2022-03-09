using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settlement : MonoBehaviour
{
    public SettlementData settlementData;
    public GameObject settlementUI;
    GameObject settUIObject;
    List<SettlementPart> settlementPartsAllowed;
    List<SettlementEvent> settlementEventsAllowed;

    private void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.name == "PlayerCharacter")
        {
            SetSettlementUI();
        }
    }

    void SetSettlementUI()
	{
        settUIObject = Instantiate(settlementUI);
        settUIObject.GetComponent<SettlementUI>().SetTitle(settlementData.settlementName);
        settUIObject.GetComponent<SettlementUI>().SetUIDataOrigin(this);

        settlementPartsAllowed = GetAllowedSettlementParts();
        
        for (int i = 0; i< settlementPartsAllowed.Count; i++)
		{
            settUIObject.GetComponent<SettlementUI>().SetPartsButton(i, settlementPartsAllowed[i].settlementPartName);
        }

        settUIObject.GetComponent<SettlementUI>().ButtonCityPartClicked(0);
    }

    public Sprite GetSettlementPartImage(int index)
	{
        return settlementPartsAllowed[index].sprite;
	}

    List<SettlementPart> GetAllowedSettlementParts()
	{
        //change later to see which part to see for player
        return settlementData.settlementParts;
	}

    public List<SettlementEvent> GetAllowedSettlementEvents(int index)
	{
        //TODO later
        return settlementPartsAllowed[index].settlementEvents;
	}
}
