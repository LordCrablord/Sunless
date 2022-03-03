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

        foreach (string t in currentItem.text)
            Debug.Log(t);

        setTextUI();
    }

    void setTextUI()
	{
        textTMP.text = currentItem.text[currentTextIndex];
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
