using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{

    public class DialogueNPC : MonoBehaviour
    {

        DialogueSystem dialogueSystem => GetComponentInChildren<DialogueSystem>();
        public GameObject dialogueObj;

        public float timerNewFrase;
        public bool hasNewFrase;
        public bool pressToInteract;
        private float m_timerNewFrase;
        private bool inRange;
        void Start()
        {
            m_timerNewFrase = timerNewFrase;
            dialogueObj = dialogueSystem.gameObject;
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
                    dialogueObj.SetActive(true);

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
            if (other.CompareTag("Player"))
            {
                if (pressToInteract)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        dialogueObj.SetActive(true);

                        //if (!dialogueObj.activeSelf)
                        //{
                        //}
                        //else if(!dialogueSystem.playingText)
                        //{
                        //    dialogueSystem.Restart(dialogueSystem.sentences);


                        //}

                        inRange = true;

                        timerNewFrase = m_timerNewFrase;
                        //dialogueSystem.Restart(dialogueSystem.sentences);

                        if (!dialogueSystem.playingText)
                        {
                            dialogueSystem.Restart(dialogueSystem.sentences);
                        }
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                inRange = false;
                //dialogueObj.SetActive(false);

            }

        }
        //public void OpenDialogue()
        //{
        //    StartCoroutine(dialogueSystem.TextDisplayCoroutine());
        //}
    }

}