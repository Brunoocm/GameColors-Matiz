using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPC : MonoBehaviour
{

    DialogueSystem dialogueSystem => GetComponentInChildren<DialogueSystem>();

    public float timerNewFrase;
    private float m_timerNewFrase;
    private bool inRange;
    void Start()
    {
        m_timerNewFrase = timerNewFrase;
    }

    void Update()
    {
        if(inRange)
        {
            if(timerNewFrase <= 0)
            {
                if (!dialogueSystem.playingText)
                {
                    dialogueSystem.Restart(dialogueSystem.sentencesBonus);
                    timerNewFrase = m_timerNewFrase;
                    inRange = false;
                }
            }
            else
            {
                timerNewFrase -= Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = true;

            timerNewFrase = m_timerNewFrase;

            if (!dialogueSystem.playingText)
            {
                dialogueSystem.Restart(dialogueSystem.sentences);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    //public void OpenDialogue()
    //{
    //    StartCoroutine(dialogueSystem.TextDisplayCoroutine());
    //}
}
