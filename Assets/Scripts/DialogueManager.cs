using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextAsset dialoguesDataJSON;
    // Start is called before the first frame update
    void Start()
    {
        DialogueData test = JsonUtility.FromJson<DialogueData>(dialoguesDataJSON.text);
        DialogueDataItem currentItem = test.items.Find(t => t.id == 1);

        foreach (string t in currentItem.text)
            Debug.Log(t);
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
        public List<string> text;
    }

}
