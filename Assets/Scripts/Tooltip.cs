using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour
{
    [SerializeField] GameObject titleTMP;
    [SerializeField] GameObject bodyTMP;

    int tooltipEnterHash = Animator.StringToHash("ObjectTooltip");
    int tooltipExitHash = Animator.StringToHash("ObjectTooltipExit");
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Animator>().Play(tooltipEnterHash);
    }


    public void SetTooltipText(string title, string mainBody)
	{
        titleTMP.GetComponent<TextMeshProUGUI>().text = title;
        bodyTMP.GetComponent<TextMeshProUGUI>().text = mainBody;
    }

    public void DestroyTooltip()
	{
        GetComponent<Animator>().Play(tooltipExitHash);
    }

    public void OnTooltipExitFinished()
	{
        Destroy(gameObject);
	}
}
