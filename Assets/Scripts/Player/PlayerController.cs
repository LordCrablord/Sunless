using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerCharacterStats defaultCharacterStats;
    PlayerCharacterStats characterStats;


    bool pauseStateToggled = false;

    void Start()
    {
        
    }

    // OnPlayerCharacterSet
    void Update()
    {
		if (Input.GetKeyDown(KeyCode.I))
		{
            GameManager.Instance.ToggleCharacterUI(GameManager.Instance.GetMainCharacter());
		}
        if (Input.GetKeyDown(KeyCode.J))
        {
            QuestManager.Instance.ToggleQuestJournalUI();
        }
		if (Input.GetKeyDown(KeyCode.Space))
		{
            SetPauseState(!pauseStateToggled);
        }
		if (Input.GetKeyDown(KeyCode.E))
		{
            OnEventTriggered();
		}
    }

    public event Notify EventTriggered;
    protected virtual void OnEventTriggered()
	{
        EventTriggered?.Invoke();
	}

    public void SetPauseState(bool state)
	{
        pauseStateToggled = state;
        if (pauseStateToggled) GameManager.Instance.PauseGameRequest++;
        else GameManager.Instance.PauseGameRequest--;
    }

}
