﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterStats characterStats;
    void Start()
    {
        characterStats = gameObject.GetComponent<CharacterStats>();

        StatModifier stat1 = new StatModifier { modifierFromID = 1, modifierTo = Stats.HP, value = 5 };
        StatModifier stat2 = new StatModifier { modifierFromID = 1, modifierTo = Stats.HP, value = 0.2f};

        characterStats.AddAdditiveModToList(stat1);
        characterStats.AddMultiplyingModToList(stat2);

        Debug.Log("Player hp:" + characterStats.Hp);

        characterStats.RemoveAdditiveModFromList(stat1);

        Debug.Log("Player hp:" + characterStats.Hp);

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
