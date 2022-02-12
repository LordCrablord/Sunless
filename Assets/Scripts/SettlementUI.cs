using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettlementUI : MonoBehaviour
{
    public GameObject titleTMP;

    //int eventMouseEnterAnimHash = Animator.StringToHash("UIEventButtonMouseEnter");
    //int eventMouseExitAnimHash = Animator.StringToHash("UIEventButtonMouseExit");
    // Start is called before the first frame update
    void Start()
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

    /*public void playMouseEnterOnEventAnim(bool mouseEntered)
	{
		if (mouseEntered) { 
        }
	}*/
}
