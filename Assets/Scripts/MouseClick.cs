using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    
    public void OnAnimationFinished()
	{
        Destroy(this.gameObject);
	}
}
