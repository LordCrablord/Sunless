using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextAsset dialoguesDataJSON;
    [SerializeField] TextAsset dialogueBranchesDataJSON;

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

    public void StartDialoguePreparations(List<DialogueDataItemString> dialogues)
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
        List<DialogueAction> currentActions = dialogueAction.GetFutureDialogueActions();
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
            case 2:
                return new BranchDialogueAction(dialogueBranchesDataJSON, actionJSON.action_id);
            default:
                return null;
		}
	}

    void CloseDialogueUI()
	{
        dialogueUI.SetActive(false);
	}
}
