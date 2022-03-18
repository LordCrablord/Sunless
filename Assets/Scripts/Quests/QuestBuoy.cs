using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBuoy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        QuestManager.Instance.CurrentlyFollowedQuestChanged += OnQuestOrQuestPartModified;
        QuestManager.Instance.TriggerManager.QuestPartAllowed += OnQuestOrQuestPartModified;
        gameObject.SetActive(false);
    }

    public void OnQuestOrQuestPartModified()
	{
        if (QuestManager.Instance.CurrentlyFollowedQuest == null) return;
        List<QuestPart> allowedParts = QuestManager.Instance.GetAllowedQuestParts(QuestManager.Instance.CurrentlyFollowedQuest);

        if (allowedParts.Count > 0)
		{
            int id = allowedParts[allowedParts.Count - 1].questPartID;
            Vector3 newPosition;
            if (QuestManager.Instance.TriggerManager.markPositions.TryGetValue(id, out newPosition))
			{
                gameObject.SetActive(true);
                transform.position = newPosition;
			}
			else
			{
                gameObject.SetActive(false);
            }
		}

		if (QuestManager.Instance.completedQuests.Contains(QuestManager.Instance.CurrentlyFollowedQuest))
		{
            gameObject.SetActive(false);
        }
	}
}
