using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

namespace OniricoStudios
{


    public class StartDesafio : MonoBehaviour
    {
        public float timerToStart;
        public float timerToTp;
        public GameObject enemy;
        public GameObject canvas;
        public Transform pos;
        public GameObject spriteEnemy;
        public GameObject SpriteNPC;
        public DialogueNPC Contagem321;

        public UnityEvent end;

        public GameObject currentEnemy;
        private Collider collDialogue => GetComponent<SphereCollider>();
        private SpriteRenderer spriteRenderer => GetComponentInChildren<SpriteRenderer>();
        MainCheckpoint mainCheckpoint => FindObjectOfType<MainCheckpoint>();

        private bool inCombat;
        public bool isFinished;
        void Start()
        {

        }

        void Update()
        {
            if (inCombat && currentEnemy == null && !isFinished)
            {
                EndDesavio();
            }

            if(CharacterStats.playerObj == null && !isFinished)
            {
                RestartDesafio();
            }
     
        }

        public void StartDesavio()
        {
            if (!BlockWayArena.completeInsignia)
            {
                mainCheckpoint.StartCoroutine(mainCheckpoint.Transition(timerToTp));

                StartCoroutine(Delay());
            }
           

        }

        IEnumerator Delay()
        {
            collDialogue.enabled = false;

            yield return new WaitForSeconds(timerToTp/2);

            CharacterStats.playerObj.transform.position = pos.position;
      
            yield return new WaitForSeconds(timerToTp / 2);

            Contagem321.StartFunction();

            yield return new WaitForSeconds(timerToStart);


            currentEnemy = Instantiate(enemy, spriteEnemy.transform.position, Quaternion.identity);
            spriteEnemy.SetActive(false);
            inCombat = true;


        }

        public void EndDesavio()
        {
            isFinished = true;
            inCombat = false;
            collDialogue.enabled = false;

            SpriteNPC.transform.DOMoveZ(SpriteNPC.transform.position.z + 7, 1);

            CompleteDesafioUI render = FindObjectOfType<CompleteDesafioUI>();
            StartCoroutine(render.SetInsignia());

            end.Invoke();
        }


        public void RestartDesafio()
        {
            inCombat = false;
            collDialogue.enabled = true;
            spriteEnemy.SetActive(true);
            Destroy(currentEnemy);
        }

        private void OnTriggerStay(Collider other)
        {
            if (!inCombat && !isFinished)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (other.gameObject.CompareTag("Player"))
                    {
                        canvas.SetActive(true);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!inCombat && !isFinished)
            {
                if (other.gameObject.CompareTag("Player"))
                {
                    canvas.SetActive(false);
                }
            }

        }
    }
}