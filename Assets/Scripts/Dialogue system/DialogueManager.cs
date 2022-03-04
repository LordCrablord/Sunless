using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextAsset dialoguesDataJSON;
    [SerializeField] GameObject dialogueUI;
    [SerializeField] TextMeshProUGUI textTMP;
    [SerializeField] TextMeshProUGUI nameTMP;
    [SerializeField] Image characterImage;

    IDialogueAction dialogueAction;
    List<DialogueDataItemString> currentDialogue;
    int currentTextIndex;
    void Start()
    {
        dialogueAction = new SimpleDialogueAction(dialoguesDataJSON, 1);
        currentDialogue = dialogueAction.GetCurrentDialogue();
        currentTextIndex = 0;

        SetUI();
        currentTextIndex++;
    }

    void SetUI()
	{
        textTMP.text = currentDialogue[currentTextIndex].text;
        nameTMP.text = currentDialogue[currentTextIndex].name;
        var image = Resources.Load<Sprite>(currentDialogue[currentTextIndex].imagePath);
        characterImage.sprite = Resources.Load<Sprite>(currentDialogue[currentTextIndex].imagePath);
    }

    public void NextDialogueString()
	{
        if (currentTextIndex == currentDialogue.Count)
		{
            ManageActions();
            return;
        }
            
        SetUI();
        currentTextIndex++;
    }

    void ManageActions()
	{
        List<DialogueAction> currentActions = dialogueAction.GetDialogueActions();
        if(currentActions.Count == 0)
		{
            CloseDialogueUI();
            return;
        }
    }

    void CloseDialogueUI()
	{
        dialogueUI.SetActive(false);
	}
}

[Serializable]
public class DialogueData
{
    public List<DialogueDataItem> items;
}
[Serializable]
public class DialogueDataItem
{
    public int id;
    public List<DialogueDataItemString> dialogue;
    public List<DialogueAction> actions;
}
[Serializable]
public class DialogueDataItemString
{
    public string name;
    public string imagePath;
    public string text;
}
[Serializable]
public class DialogueAction
{
    public int action_type;
    public int action_id;
}