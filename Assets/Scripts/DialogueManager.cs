using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextAsset dialoguesDataJSON;
    [SerializeField] TextMeshProUGUI textTMP;
    [SerializeField] TextMeshProUGUI nameTMP;
    [SerializeField] Image characterImage;

    DialogueData test;
    DialogueDataItem currentItem;
    int currentTextIndex;
    void Start()
    {
        test = JsonUtility.FromJson<DialogueData>(dialoguesDataJSON.text);
        currentItem = test.items.Find(t => t.id == 1);
        currentTextIndex = 0;

        setUI();
    }

    void setUI()
	{
        textTMP.text = currentItem.dialogue[currentTextIndex].text;
        nameTMP.text = currentItem.dialogue[currentTextIndex].name;
        var image = Resources.Load<Sprite>(currentItem.dialogue[currentTextIndex].imagePath);
        characterImage.sprite = Resources.Load<Sprite>(currentItem.dialogue[currentTextIndex].imagePath);
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
        public string imagePath;
        public string text;
    }

}
