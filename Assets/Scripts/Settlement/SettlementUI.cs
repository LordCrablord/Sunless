using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SettlementUI : MonoBehaviour
{
    public GameObject titleTMP;
    Settlement settlement;

    [SerializeField] GameObject buttonPrefab;
    [SerializeField] GameObject buttonContainer;
    [SerializeField] GameObject eventUIPrefab;
    [SerializeField] GameObject eventContainer;

    [SerializeField] Image Image;

    List<GameObject> buttons = new List<GameObject>();
    List<GameObject> eventButtons = new List<GameObject>();

	public void SetUIDataOrigin(Settlement data)
	{
        settlement = data;
	}

    public void SetTitle(string title)
	{
        titleTMP.GetComponent<TextMeshProUGUI>().text = title;
	}

    public void SetPartsButton(int index, string partName)
	{
        GameObject newButton = Instantiate(buttonPrefab, transform.position, Quaternion.identity);
        newButton.transform.SetParent(buttonContainer.transform, false);
        newButton.GetComponent<Button>().onClick.AddListener(delegate { ButtonCityPartClicked(index); });
        newButton.GetComponentInChildren<TextMeshProUGUI>().text = partName;
        buttons.Add(newButton);
    }

    public void ButtonCityPartClicked(int index)
	{
        ResetEventButtons();

        Image.sprite = settlement.GetSettlementPartImage(index);

        List<SettlementEvent> settlementEvents = settlement.GetAllowedSettlementEvents(index);
	    foreach(SettlementEvent myEvent in settlementEvents)
		{
            GameObject eventButton = Instantiate(eventUIPrefab, transform.position, Quaternion.identity);
            eventButton.transform.SetParent(eventContainer.transform, false);
            eventButton.GetComponent<SettlementEventUI>().SetEventButton(myEvent);
            eventButtons.Add(eventButton);
		}
    }

    void ResetEventButtons()
	{
        foreach (GameObject button in eventButtons)
        {
            Destroy(button);
        }
	}

    public void ResetUI()
	{
        foreach(GameObject button in buttons)
		{
            Destroy(button);
		}
	}

    public void CloseUI()
	{
        Destroy(gameObject);
	}

}
