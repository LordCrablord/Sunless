using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextAsset dialoguesDataJSON;
    [SerializeField] GameObject dialogueUI;
    [SerializeField] TextMeshProUGUI textTMP;
    [SerializeField] TextMeshProUGUI nameTMP;
    [SerializeField] Image characterImage;

    IDialogueAction dialogueAction;
    List<DialogueDataItemString> currentDialogues;
    int currentTextIndex;
    void Start()
    {
        dialogueAction = new SimpleDialogueAction(dialoguesDataJSON, 1);
        dialogueAction.DoAction(this.gameObject);
    }

    public void PrepareUIForDialogue(List<DialogueDataItemString> dialogues)
	{
        currentDialogues = dialogues;
        currentTextIndex = 0;

        SetUI();
        currentTextIndex++;
    }

    void SetUI()
	{
        textTMP.text = currentDialogues[currentTextIndex].text;
        nameTMP.text = currentDialogues[currentTextIndex].name;
        var image = Resources.Load<Sprite>(currentDialogues[currentTextIndex].imagePath);
        characterImage.sprite = Resources.Load<Sprite>(currentDialogues[currentTextIndex].imagePath);
    }

    public void NextDialogueString()
	{
        if (currentTextIndex == currentDialogues.Count)
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
        for(int i = 0; i<currentActions.Count; i++)
		{
            IDialogueAction currentDialogueAction = GetDialogueAction(currentActions[i]);
            if (i == currentActions.Count - 1)
            {
                dialogueAction = currentDialogueAction;
            }

            currentDialogueAction.DoAction(this.gameObject);
        }
    }

    IDialogueAction GetDialogueAction(DialogueAction actionJSON)
	{
        switch (actionJSON.action_type)
		{
            case 1:
                return new SimpleDialogueAction(dialoguesDataJSON, actionJSON.action_id);
            default:
                return null;
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