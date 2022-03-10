using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestPartUI : MonoBehaviour
{
    [SerializeField] float defaultHeight = 80;
    [SerializeField] float toggledHeight = 280;
    [SerializeField] TextMeshProUGUI questPartTitleTMP;

    bool isToggled = false;
    [SerializeField] TextMeshProUGUI descriptionTMP;

    public void OnQuestPartClicked()
	{
        isToggled = !isToggled;
        descriptionTMP.gameObject.SetActive(isToggled);
        float currentHeight = isToggled ? toggledHeight : defaultHeight;
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, currentHeight);
	}

    public void SetQuestPartUI(QuestPart questPart)
	{
        questPartTitleTMP.text = questPart.partTitle;
        descriptionTMP.text = questPart.description;
	}
}
