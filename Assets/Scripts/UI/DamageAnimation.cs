using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageAnimation : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI damageTMP;
	[SerializeField] Animator animator;

   public void StartAnimation(DamageEventArgs e)
	{
		damageTMP.text = e.damage.ToString();
		if (e.isCrit)
			animator.Play("Damage Animation");
		else
			animator.Play("Damage Animation");
	}

	public void OnAnimationComplete()
	{
		Destroy(gameObject);
	}
}
