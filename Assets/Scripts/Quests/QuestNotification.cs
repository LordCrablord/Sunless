using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestNotification : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questTMP;
    [SerializeField] TextMeshProUGUI questPartTMP;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void PlayNotification(string questString, string questPartString)
    {
        questTMP.text = questString;
        questPartTMP.text = questPartString;
        animator.Play("QuestStarted");
    }

    public void OnAnimFinished()
    {
        gameObject.SetActive(false);
    }
    
}
