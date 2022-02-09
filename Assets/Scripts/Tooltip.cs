using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour
{
    [SerializeField] GameObject titleTMP;
    [SerializeField] GameObject bodyTMP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTooltipText(string title, string mainBody)
	{
        titleTMP.GetComponent<TextMeshProUGUI>().text = title;
        bodyTMP.GetComponent<TextMeshProUGUI>().text = mainBody;
    }
}
