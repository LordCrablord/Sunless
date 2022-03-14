using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageAnimation : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI damageTMP;
	[SerializeField] Animator animator;
	[SerializeField] Color damageColor;
	[SerializeField] Color healColor;
	[SerializeField] Color damageCritColor;
	float textSizeOnCrit = 60;

   public void StartAnimation(DamageEventArgs e)
	{
		damageTMP.text = e.damage.ToString();

		if (e.damage >= 0)
			damageTMP.color = damageColor;
		else
			damageTMP.color = healColor;

		if (e.isCrit)
		{
			damageTMP.color = damageCritColor;
			damageTMP.fontSize = textSizeOnCrit;
		}
			

		if (e.isCrit)
			animator.Play("Damage Crit Animation");
		else
			animator.Play("Damage Animation");
	}

	public void OnAnimationComplete()
	{
		Destroy(gameObject);
	}
}
