using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestPartUI : MonoBehaviour
{
    [SerializeField] float defaultHeight = 80;
    [SerializeField] float toggledHeight = 280;

    bool isToggled = false;
    [SerializeField] TextMeshProUGUI descriptionTMP;

    public void OnQuestPartClicked()
	{
        isToggled = !isToggled;
        descriptionTMP.gameObject.SetActive(isToggled);
        float currentHeight = isToggled ? toggledHeight : defaultHeight;
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentHeight);
	}
}
