using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterStats characterStats;
    void Start()
    {
        characterStats = gameObject.GetComponent<CharacterStats>();

        characterStats.AddAdditiveModifierToList(new StatModifier { modifierFromID = 1, modifierTo = Stats.HP, value = 5 });
        characterStats.AddMultiplyingModifierToList(new StatModifier { modifierFromID = 1, modifierTo = Stats.HP, value = 0.2f });

        Debug.Log("Player hp:" + characterStats.Hp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
