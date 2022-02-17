using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] TextAsset dialoguesDataJSON;
    // Start is called before the first frame update
    void Start()
    {
        DialogueData test = JsonUtility.FromJson<DialogueData>(dialoguesDataJSON.text);
        Debug.Log(test.id);
        foreach(string t in test.text)
            Debug.Log(t);
    }

    private class DialogueData
	{
        public int id;
        public List<string> text;
	}
}
