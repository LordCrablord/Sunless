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

    [SerializeField] Image Image;

    List<GameObject> buttons;
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
    }

    public void ButtonCityPartClicked(int index)
	{
        SettlementPart settlementPart = settlement.GetSettlementPartData(index);
        Image.sprite = settlementPart.sprite;
        
        //settlement Send data plz
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
