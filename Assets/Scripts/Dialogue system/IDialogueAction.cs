using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDialogueAction
{
    void ManageAction();
    List<DialogueDataItemString> GetCurrentDialogue();
}
