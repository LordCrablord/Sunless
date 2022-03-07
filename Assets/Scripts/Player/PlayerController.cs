using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    PlayerCharacterStats characterStats;
    void Start()
    {
        characterStats = gameObject.GetComponent<PlayerCharacterStats>();

        StatModifier stat1 = new StatModifier { modifierFromID = 1, modifierTo = Stats.XP, value = 5 };
        StatModifier stat2 = new StatModifier { modifierFromID = 1, modifierTo = Stats.HP_MAX, value = 0.2f};
        StatModifier stat3 = new StatModifier { modifierFromID = 1, modifierTo = Stats.GOLD, value = 0.05f };
        characterStats.AddAdditiveModToList(stat1);
        characterStats.AddMultiplyingModToList(stat2);
        characterStats.AddMultiplyingModToList(stat3);

        characterStats.Xp += 185;
        characterStats.Gold += 1500;
        Debug.Log("Gold: " + characterStats.Gold);
        PlayerCharacterStats otherCharacter = new PlayerCharacterStats();
        Debug.Log("Gold of other character: " + otherCharacter.Gold);


        characterStats.Hp += 400;
        

        GameManager.Instance.SetCharacterDataOnUI(characterStats);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
