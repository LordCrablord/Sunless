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
            settUIObject = Instantiate(settlementUI);
            settUIObject.GetComponent<SettlementUI>().SetTitle(settlementData.settlementName);
            
        }
    }
}
