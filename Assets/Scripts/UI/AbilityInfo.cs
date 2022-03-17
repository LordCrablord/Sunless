using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilityInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI abilityName;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] TextMeshProUGUI apCostTMP;
    [SerializeField] GameObject from0;
    [SerializeField] GameObject from1;
    [SerializeField] GameObject from2;

    [SerializeField] GameObject ally0;
    [SerializeField] GameObject ally1;
    [SerializeField] GameObject ally2;

    [SerializeField] GameObject enemy0;
    [SerializeField] GameObject enemy1;
    [SerializeField] GameObject enemy2;

    public void SetAbilityTooltip(Ability ability)
	{
        if (ability == null) return;
        abilityName.text = ability.abilityName;
        description.text = ability.description;
        apCostTMP.text = "AP: " + ability.apCost;

		if (!ability.allowedFromPosition.Contains(TargetPosition.NONE)&& 
            ability.allowedFromPosition.Count>0)
		{
            if (ability.allowedFromPosition.Contains(TargetPosition.POS_ZERO))
                from2.SetActive(true);
            if (ability.allowedFromPosition.Contains(TargetPosition.POS_ONE))
                from1.SetActive(true);
            if (ability.allowedFromPosition.Contains(TargetPosition.POS_TWO))
                from0.SetActive(true);
        }
        if (!ability.targetAlly.Contains(TargetPosition.NONE) &&
            ability.targetAlly.Count > 0)
        {
            if (ability.targetAlly.Contains(TargetPosition.POS_ZERO))
                ally2.SetActive(true);
            if (ability.targetAlly.Contains(TargetPosition.POS_ONE))
                ally1.SetActive(true);
            if (ability.targetAlly.Contains(TargetPosition.POS_TWO))
                ally0.SetActive(true);
        }
        if (!ability.targetEnemy.Contains(TargetPosition.NONE) &&
            ability.targetEnemy.Count > 0)
        {
            if (ability.targetEnemy.Contains(TargetPosition.POS_ZERO))
                enemy0.SetActive(true);
            if (ability.targetEnemy.Contains(TargetPosition.POS_ONE))
                enemy1.SetActive(true);
            if (ability.targetEnemy.Contains(TargetPosition.POS_TWO))
                enemy2.SetActive(true);
        }
    }
}
