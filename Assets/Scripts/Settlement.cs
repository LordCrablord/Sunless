using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settlement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

	private void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.name == "PlayerCharacter")
        {
            print("Player entered the city");
        }
    }
}
