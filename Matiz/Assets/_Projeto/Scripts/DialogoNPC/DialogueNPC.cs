using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPC : MonoBehaviour
{

    DialogueSystem dialogueSystem => GetComponentInChildren<DialogueSystem>();
    void Start()
    {
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!dialogueSystem.playingText)
            {
                OpenDialogue();
            }
        }
    }

    public void OpenDialogue()
    {
        StartCoroutine(dialogueSystem.TextDisplayCoroutine());
    }
}
