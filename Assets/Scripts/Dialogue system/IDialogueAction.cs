using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogueAction
{
    void DoAction(GameObject dialogueManager);
    List<DialogueAction> GetDialogueActions();
}
