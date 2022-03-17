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
            if(button != buttons[0])
                button.GetComponent<Button>().onClick.RemoveAllListeners();
            
            button.SetActive(false);
		}
	}

    void SetContinueButton()
	{
        buttons[0].SetActive(true);
        //buttons[0].GetComponent<Button>().onClick.RemoveAllListeners();
        //buttons[0].GetComponent<Button>().onClick.AddListener(NextDialogueString);
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
        currentTextIndex++;
        if (currentTextIndex == currentDialogues.Count)
		{
            ManageActions();
            return;
        }

        SetUI();
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

            if(currentDialogueAction!=null)
                currentDialogueAction.DoAction(this.gameObject);
        }
    }

    enum DialogueActionTypes {NONE, SIMPLE, BRANCH, RANDOM, 
        STAT_CHANGE, ADD_FORBID_SETTL_TRIGGER, REMOVE_FORBID_SETTL_TRIGGER,
        START_QUEST, START_BATTLE, CLOSE_DIALOGUE}
    IDialogueAction GetDialogueAction(DialogueAction actionJSON)
	{
        switch ((DialogueActionTypes)actionJSON.action_type)
		{
            case DialogueActionTypes.SIMPLE:
                return new SimpleDialogueAction(dialoguesDataJSON, actionJSON.action_id);
            case DialogueActionTypes.BRANCH:
                return new BranchDialogueAction(dialogueBranchesDataJSON, actionJSON.action_id);
            case DialogueActionTypes.RANDOM:
                return new RandomDialogueAction(dialogueRandomDataJSON, actionJSON.action_id);
            case DialogueActionTypes.STAT_CHANGE:
                return new DialogueStatChangeAction(dialogueStatChangeJSON, actionJSON.action_id);
            case DialogueActionTypes.ADD_FORBID_SETTL_TRIGGER:
                QuestManager.Instance.TriggerManager.AddToConditionForbidList(actionJSON.action_id);
                return null;
            case DialogueActionTypes.REMOVE_FORBID_SETTL_TRIGGER:
                QuestManager.Instance.TriggerManager.RemoveFromConditionForbidList(actionJSON.action_id);
                return null;
            case DialogueActionTypes.START_QUEST:
                QuestManager.Instance.AddToActiveQuests(actionJSON.action_id);
                return null;
            case DialogueActionTypes.START_BATTLE:
                BattleManager.Instance.PrepareBattle(GameManager.Instance.BattleDatabase.battles.Find(b=>b.battleID == actionJSON.action_id));
                return null;
            case DialogueActionTypes.CLOSE_DIALOGUE:
                CloseDialogueUI();
                return null;
            default:
                return null;
		}
	}

    public void StartDialogue(DialogueAction newDialogueAction)
	{
        dialogueUI.SetActive(true);
        GameManager.Instance.PauseGameRequest++;
        dialogueAction = GetDialogueAction(newDialogueAction);
        dialogueAction.DoAction(this.gameObject);
    }

    void CloseDialogueUI()
	{
        dialogueUI.SetActive(false);
        GameManager.Instance.PauseGameRequest--;
	}
}
