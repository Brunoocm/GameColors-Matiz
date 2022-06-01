using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{

    public class DialogueNPC : MonoBehaviour
    {            
        [Header("Dialogue")]
        public GameObject dialogueObj;

        public float timerNewFrase;
        public bool hasNewFrase;
        public bool pressToInteract;
        private float m_timerNewFrase;
        private bool inRange;

        [Header("Different Dialogue Arenas")]
        public bool newFraseForFirstArena;
        public bool newFraseForSecondArena;

        DialogueSystem dialogueSystem => GetComponentInChildren<DialogueSystem>();
        ProgressionManager progressionManager => FindObjectOfType<ProgressionManager>();
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

        public void WhichTextDisplay()
        {
            if (!dialogueSystem.playingText)
            {
                if (newFraseForSecondArena && progressionManager.firstArena)                //se ele terminou o desafio e tem frase pra primeira arena  
                {
                    dialogueSystem.Restart(dialogueSystem.sentencesForSecondArena);
                }
                else if (newFraseForFirstArena && progressionManager.desafio)                //se ele terminou a primeira arena e tem frase pra segunda arena
                {
                    dialogueSystem.Restart(dialogueSystem.sentencesForFirstArena);
                }         
                else
                {
                    dialogueSystem.Restart(dialogueSystem.sentences);
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (!pressToInteract)
                {
                    //dialogueObj.SetActive(true);

                    inRange = true;
                    WhichTextDisplay();
                    timerNewFrase = m_timerNewFrase;

                   
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