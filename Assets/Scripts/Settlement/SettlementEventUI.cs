using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettlementEventUI : MonoBehaviour
{
    SettlementEvent settlementEvent;
    [SerializeField] TextMeshProUGUI eventName;
    [SerializeField] Image image;

    public void SetEventButton(SettlementEvent thisEvent)
	{
        settlementEvent = thisEvent;
        eventName.text = settlementEvent.eventName;
        image.sprite = settlementEvent.sprite;
	}
}
