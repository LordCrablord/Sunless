using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    [SerializeField] PlayerCharacterStats mainCharacterStartingStats;
    PlayerCharacterStats mainCharacter;
    public PlayerCharacterStats MainCharacter { get { return mainCharacter; } }
    [SerializeField] List<PlayerCharacterStats> playerPCDatabase;
    public PlayerCharacterStats[] party = new PlayerCharacterStats[3];

	void Start()
	{
        mainCharacter = Instantiate(mainCharacterStartingStats);
        party[0] = mainCharacter;
        SetPositionInCharStats();

        StatModifier stat1 = new StatModifier { modifierFromID = 1, modifierTo = Stats.XP, value = 5 };
        mainCharacter.AddAdditiveModToList(stat1);

        mainCharacter.Xp += 185;
        mainCharacter.Gold += 1500;

        PlayerCharacterStats otherCharacter = new PlayerCharacterStats();

        mainCharacter.Hp += 400;

        Armor newArmor = (Armor)GameManager.Instance.ItemDatabase.items[2];
        mainCharacter.InventoryBack.Add(newArmor);
        mainCharacter.EquipItem(newArmor);

        Weapon newWeapon = (Weapon)GameManager.Instance.ItemDatabase.items[4];
        mainCharacter.InventoryBack.Add(newWeapon);
        mainCharacter.EquipItem(newWeapon);
        newWeapon = (Weapon)GameManager.Instance.ItemDatabase.items[5];
        mainCharacter.InventoryBack.Add(newWeapon);
        mainCharacter.InventoryBack.Add(newWeapon);


        Debug.Log("Current Xp: " + mainCharacter.StatsDictionary[Stats.XP].Get());
        Debug.Log("Current Damage: " + mainCharacter.StatsDictionary[Stats.DAMAGE_MIN].Get());
        Debug.Log((mainCharacter.StatsDictionary[Stats.XP].Get().GetType()));
        mainCharacter.StatsDictionary[Stats.XP].Set((float)mainCharacter.StatsDictionary[Stats.XP].Get() + 5); ;
        Debug.Log("Current Xp: " + mainCharacter.StatsDictionary[Stats.XP].Get());



        GameManager.Instance.SetCharacterDataOnUI(mainCharacter);
    }

    void SetPositionInCharStats()
	{
        for(int i = 0; i<party.Length; i++)
		{
            if (party[i] != null)
                party[i].Position = i;
		}
	}

    public void ModifyMainCharacterStats(Stats stat, float value)
    {
        mainCharacter.ModifyStats(stat, value);
    }
}
