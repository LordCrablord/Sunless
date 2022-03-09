using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settlement : MonoBehaviour
{
    public SettlementData settlementData;
    public GameObject settlementUI;
    GameObject settUIObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

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

        //change later to see which part to see for player
        for (int i = 0; i< settlementData.settlementParts.Count; i++)
		{
            settUIObject.GetComponent<SettlementUI>().SetPartsButton(i, settlementData.settlementParts[i].settlementPartName);
        }

        settUIObject.GetComponent<SettlementUI>().ButtonCityPartClicked(0);
    }

    public SettlementPart GetSettlementPartData(int index)
	{
        return settlementData.settlementParts[index];
	}
}
