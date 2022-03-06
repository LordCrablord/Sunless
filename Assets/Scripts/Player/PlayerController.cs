using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterStats characterStats;
    void Start()
    {
        characterStats = gameObject.GetComponent<CharacterStats>();

        StatModifier stat1 = new StatModifier { modifierFromID = 1, modifierTo = Stats.HP_MAX, value = 5 };
        StatModifier stat2 = new StatModifier { modifierFromID = 1, modifierTo = Stats.HP_MAX, value = 0.2f};

        characterStats.AddAdditiveModToList(stat1);
        characterStats.AddMultiplyingModToList(stat2);

        Debug.Log("Player hp:" + characterStats.HpMax);

        characterStats.RemoveAdditiveModFromList(stat1);

        Debug.Log("Player hp:" + characterStats.HpMax);

        GameManager.Instance.SetCharacterDataOnUI(characterStats);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
