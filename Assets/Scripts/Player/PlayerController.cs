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
        characterStats.InventoryBack.Add(newArmor);

        Weapon newWeapon = (Weapon)GameManager.Instance.ItemDatabase.items[4];
        characterStats.InventoryBack.Add(newWeapon);
        newWeapon = (Weapon)GameManager.Instance.ItemDatabase.items[5];
        characterStats.InventoryBack.Add(newWeapon);
        characterStats.InventoryBack.Add(newWeapon);

        Debug.Log("Current Xp: " + characterStats.StatsDictionary[Stats.XP].Get());
        Debug.Log("Current Damage: " + characterStats.StatsDictionary[Stats.DAMAGE].Get());
        Debug.Log((characterStats.StatsDictionary[Stats.XP].Get().GetType()));
        characterStats.StatsDictionary[Stats.XP].Set((float)characterStats.StatsDictionary[Stats.XP].Get() + 5); ;
        Debug.Log("Current Xp: " + characterStats.StatsDictionary[Stats.XP].Get());



        GameManager.Instance.SetCharacterDataOnUI(characterStats);
    }

    void Update()
    {
		if (Input.GetKeyDown(KeyCode.I))
		{
            GameManager.Instance.ToggleCharacterUI(characterStats);
		}
    }

}
