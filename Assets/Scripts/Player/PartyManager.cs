using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PartyManager : MonoBehaviour
{
    [SerializeField] string mainCharacterName;
    PlayerCharacterStats mainCharacter;
    public PlayerCharacterStats MainCharacter { get { return mainCharacter; } }
    [SerializeField] List<PlayerCharacterStats> playerPCDatabase;
    List<PlayerCharacterStats> interactedPlayerPCDatabase = new List<PlayerCharacterStats>();
    public PlayerCharacterStats[] partyPreload = new PlayerCharacterStats[3];
    public PlayerCharacterStats[] party = new PlayerCharacterStats[3];

	void Start()
	{
        InstansiateParty();
        SetPositionInCharStats();

        //the rest here is for testing purpose

        mainCharacter.Xp += 15;
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
        newWeapon = (Weapon)GameManager.Instance.ItemDatabase.items[6];
        mainCharacter.InventoryBack.Add(newWeapon);

        Debug.Log(mainCharacter.LevelUpPoints);

        GameManager.Instance.SetCharacterDataOnUI(mainCharacter);
    }

    void InstansiateParty()
	{
        for(int i = 0; i<partyPreload.Length; i++)
		{
            if (partyPreload[i] != null)
			{
                party[i] = Instantiate(partyPreload[i]);
                if (party[i].characterName == mainCharacterName)
                    mainCharacter = party[i];
            }   
		}
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

    public void SwapPositions(int pos1, int pos2)
	{
        PlayerCharacterStats temp = party[pos1];
        party[pos1] = party[pos2];
        party[pos2] = temp;
        SetPositionInCharStats();
	}

    public void AddCharacterToParty(int charListID)
	{
        PlayerCharacterStats companion;
        if (interactedPlayerPCDatabase.Find(x => x.Id == charListID) != null)
		{
            companion = interactedPlayerPCDatabase.Find(x => x.Id == charListID);
            interactedPlayerPCDatabase.Remove(companion);
		}
		else
		{
            companion = Instantiate(playerPCDatabase[charListID]);
        }

		if (companion.PersonalCharacterLevel < companion.Level)
		{
            while(companion.PersonalCharacterLevel < companion.Level)
			{
                companion.LevelUpPoints += 2;
                companion.AbilityToLearn++;
                companion.PersonalCharacterLevel++;
            }
		}

        companion.Hp = companion.HpMax;

        for (int i = 0; i<party.Length; i++)
		{
			if (party[i] == null)
			{
                party[i] = companion;
                companion.Position = i;
                return;
			}
		}
	}

    public void RemoveCharacterFromParty(int charID)
    {
        for (int i = 0; i < party.Length; i++)
        {
            if (party[i] != null)
                if (party[i].Id == charID)
				{
                    interactedPlayerPCDatabase.Add(party[i]);
                    party[i] = null;
                }
            GameManager.Instance.SetCharacterDataOnUI(mainCharacter);
        }
    }

    public void PartyLevelUp()
	{
        foreach(PlayerCharacterStats playerCharacter in party)
		{
			if (playerCharacter != null)
			{
                playerCharacter.LevelUpPoints += 2;
                playerCharacter.AbilityToLearn++;
                playerCharacter.PersonalCharacterLevel = (int)playerCharacter.Level;
            }   
		}
	}

    public bool CheckIfInParty(int ID)
	{
        for(int i = 0; i < party.Length; i++)
		{
            if (party[i] != null)
                if (party[i].Id == ID)
                    return true;
		}
        return false;
	}

    public bool CheckForFreeSpace()
	{
        for (int i = 0; i < party.Length; i++)
        {
            if (party[i] == null)
                return true;
        }
        return false;
    }
}
