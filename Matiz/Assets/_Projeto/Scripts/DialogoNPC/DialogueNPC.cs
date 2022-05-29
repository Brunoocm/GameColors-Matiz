using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{

    public class DialogueNPC : MonoBehaviour
    {

        DialogueSystem dialogueSystem => GetComponentInChildren<DialogueSystem>();

        public float timerNewFrase;
        public bool hasNewFrase;
        public bool pressToInteract;
        private float m_timerNewFrase;
        private bool inRange;
        void Start()
        {
            m_timerNewFrase = timerNewFrase;
        }

        void Update()
        {
            if (inRange)
            {
                if (timerNewFrase <= 0 && hasNewFrase)
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

        public void StartFunction()
        {
            inRange = true;

            timerNewFrase = m_timerNewFrase;

            if (!dialogueSystem.playingText)
            {
                dialogueSystem.Restart(dialogueSystem.sentences);
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (!pressToInteract)
                {
                    inRange = true;

                    timerNewFrase = m_timerNewFrase;

                    if (!dialogueSystem.playingText)
                    {
                        dialogueSystem.Restart(dialogueSystem.sentences);
                    }
                }
                else
                {

                }
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (pressToInteract)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    inRange = true;

                    timerNewFrase = m_timerNewFrase;

                    if (!dialogueSystem.playingText)
                    {
                        dialogueSystem.Restart(dialogueSystem.sentences);
                    }
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

}