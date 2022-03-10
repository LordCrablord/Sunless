using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Settlement : MonoBehaviour
{
    public SettlementData settlementData;
    public GameObject settlementUI;
    GameObject settUIObject;
    List<SettlementPart> settlementPartsAllowed;
    List<SettlementEvent> settlementEventsAllowed;

    public int settlementPartIndex;
    private void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.name == "PlayerCharacter")
        {
            QuestManager.Instance.TriggerManager.SettlementTriggerListsModified += TriggerListModified;
            SetSettlementUI();
        }
    }

    public void TriggerListModified()
	{
        settUIObject.GetComponent<SettlementUI>().ClearUI();
        SetUpUI();
	}

    public void OnSettlementExit()
	{
        QuestManager.Instance.TriggerManager.SettlementTriggerListsModified -= TriggerListModified;
    }

    void SetSettlementUI()
	{
        settUIObject = Instantiate(settlementUI);
        settUIObject.GetComponent<SettlementUI>().SetTitle(settlementData.settlementName);
        settUIObject.GetComponent<SettlementUI>().SetUIDataOrigin(this);
        settlementPartIndex = 0;

        SetUpUI();
    }

    void SetUpUI()
	{
        settlementPartsAllowed = GetAllowedSettlementParts();

        for (int i = 0; i < settlementPartsAllowed.Count; i++)
        {
            settUIObject.GetComponent<SettlementUI>().SetPartsButton(i, settlementPartsAllowed[i].settlementPartName);
        }

        settUIObject.GetComponent<SettlementUI>().ButtonCityPartClicked(settlementPartIndex);
    }

    public Sprite GetSettlementPartImage(int index)
	{
        return settlementPartsAllowed[index].sprite;
	}

    List<SettlementPart> GetAllowedSettlementParts()
	{
        return settlementData.settlementParts.Where(r => !QuestManager.Instance.TriggerManager.settlementConditionForbidID.Contains(r.id)).ToList();
	}

    public List<SettlementEvent> GetAllowedSettlementEvents(int index)
	{
        List<SettlementEvent> res = settlementPartsAllowed[index].settlementEvents.Where(
            r=>!QuestManager.Instance.TriggerManager.settlementConditionForbidID.Contains(r.eventID)).ToList();
        Debug.Log(res.Count);
        return res;
	}
}
