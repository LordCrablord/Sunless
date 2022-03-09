using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextAsset dialoguesDataJSON;
    [SerializeField] TextAsset dialogueBranchesDataJSON;
    [SerializeField] TextAsset dialogueRandomDataJSON;
    [SerializeField] TextAsset dialogueStatChangeJSON;

    [SerializeField] GameObject dialogueUI;
    [SerializeField] TextMeshProUGUI textTMP;
    [SerializeField] TextMeshProUGUI nameTMP;
    [SerializeField] Image characterImage;

    [SerializeField] List<GameObject> buttons;

    IDialogueAction dialogueAction;
    List<DialogueDataItemString> currentDialogues;
    int currentTextIndex;

    public void ResetButtons()
	{
        foreach(GameObject button in buttons)
		{
            button.GetComponent<Button>().onClick.RemoveAllListeners();
            button.SetActive(false);
		}
	}

    void SetContinueButton()
	{
        buttons[0].SetActive(true);
        buttons[0].GetComponent<Button>().onClick.AddListener(NextDialogueString);
	}

    public void StartDialogueSetup(List<DialogueDataItemString> dialogues)
	{
        currentDialogues = dialogues;
        currentTextIndex = 0;

        SetUI();
    }

    public void SetSimpleDialogueUISetup(List<DialogueDataItemString> dialogues)
    {
        ResetButtons();
        SetContinueButton();
        StartDialogueSetup(dialogues);
    }

    public List<GameObject> GetButtons()
	{
        return buttons;
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

    public void ManageActions()
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
            case 3:
                return new RandomDialogueAction(dialogueRandomDataJSON, actionJSON.action_id);
            case 4:
                return new DialogueStatChangeAction(dialogueStatChangeJSON, actionJSON.action_id);
            default:
                return null;
		}
	}

    public void StartDialogue(DialogueAction newDialogueAction)
	{
        dialogueUI.SetActive(true);
        dialogueAction = GetDialogueAction(newDialogueAction);
        dialogueAction.DoAction(this.gameObject);
    }

    void CloseDialogueUI()
	{
        dialogueUI.SetActive(false);
	}
}
