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
        DialogueData test;

        //DialogueData test = JsonUtility.FromJson<DialogueData>(dialoguesDataJSON.text);
        //foreach (string t in test.text)
        //    Debug.Log(t);
    }

    private struct DialogueData
	{
        public List<string> text;
	}

    /*public class Demo
    {
        public void ReadJSON()
        {
            string path = Application.streamingAssetsPath + "/Player.json";
            string JSONString = File.ReadAllText(path);
            Player player = JsonUtility.FromJson<Player>(JSONString);
            Debug.Log(player.Name);
        }
    }

    [System.Serializable]
    public class Player
    {
        public string Name;
        public int Level;
    }*/

    //2
    /*
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }*/
}
