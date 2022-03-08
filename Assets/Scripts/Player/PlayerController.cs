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

        PlayerCharacterStats otherCharacter = new PlayerCharacterStats();

        characterStats.Hp += 400;

        Armor newArmor = (Armor) GameManager.Instance.ItemDatabase.items[2];
        Debug.Log(newArmor.id + ", name: " + newArmor.name + ", value: " + newArmor.armorValue);

        characterStats.Inventory.Add(newArmor);

        /*Item temp = new Weapon();
		switch (temp)
		{
            case Weapon w: 
                Debug.Log("this is weapon");
                break;
            case Armor a:
                Debug.Log("this is armor");
                break;
            default: 
                Debug.Log("No type recognized"); 
                break;
		}*/



        GameManager.Instance.SetCharacterDataOnUI(characterStats);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
