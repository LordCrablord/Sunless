using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMarkScript : MonoBehaviour
{
    [SerializeField] List<int> markForQuestsWithID = new List<int>();
    void Start()
    {
        foreach(int i in markForQuestsWithID)
            QuestManager.Instance.TriggerManager.markPositions.Add(i, gameObject.transform.position);
    }

}
