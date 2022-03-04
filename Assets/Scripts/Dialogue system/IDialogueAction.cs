using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogueAction
{
    void DoAction(GameObject dialogueManager);
    List<DialogueDataItemString> GetCurrentDialogue();
    List<DialogueAction> GetDialogueActions();
}
