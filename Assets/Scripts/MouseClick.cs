using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
	private void Start()
	{
		GetComponent<Animator>().Play("MousePointClick");
	}
	public void OnAnimationFinished()
	{
        Destroy(this.gameObject);
	}
}
