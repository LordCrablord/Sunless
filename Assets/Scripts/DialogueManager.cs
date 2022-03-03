using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextAsset dialoguesDataJSON;
    [SerializeField] TextMeshProUGUI textTMP;

    DialogueData test;
    DialogueDataItem currentItem;
    int currentTextIndex;
    void Start()
    {
        test = JsonUtility.FromJson<DialogueData>(dialoguesDataJSON.text);
        currentItem = test.items.Find(t => t.id == 1);
        currentTextIndex = 0;

        setTextUI();
    }

    void setTextUI()
	{
        textTMP.text = currentItem.dialogue[currentTextIndex].text;
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
    }
    [Serializable]
    public class DialogueDataItemString
    {
        public string name;
        public string text;
    }

}
