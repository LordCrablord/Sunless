using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeUI : MonoBehaviour
{
    [SerializeField] InventoryUI playerInventoryUI;
    public List<Item> tempItems;

    public void SetTradeUI()
	{
        playerInventoryUI.GetComponent<InventoryUI>().SetInventory(tempItems);
	}
	private void Start()
	{
		SetTradeUI();
	}
}
