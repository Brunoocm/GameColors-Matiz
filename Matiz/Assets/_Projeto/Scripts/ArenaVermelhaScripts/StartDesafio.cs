using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OniricoStudios
{


    public class StartDesafio : MonoBehaviour
    {
        public GameObject barrier;
        public GameObject enemy;
        public GameObject canvas;

        public UnityEvent end;

        public GameObject currentEnemy;
        private Collider collDialogue => GetComponent<SphereCollider>();
        private SpriteRenderer spriteRenderer => GetComponentInChildren<SpriteRenderer>();

        private bool inCombat;
        private bool isFinished;
        void Start()
        {

        }

        void Update()
        {
            if (inCombat && currentEnemy == null && !isFinished)
            {
                EndDesavio();
            }
     
        }

        public void StartDesavio()
        {
            StartCoroutine(Delay());
           

        }

        IEnumerator Delay()
        {
            collDialogue.enabled = false;
            barrier.SetActive(true);

            yield return new WaitForSeconds(3);

            spriteRenderer.enabled = false;
            currentEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
            inCombat = true;


        }

        public void EndDesavio()
        {
            isFinished = true;
            inCombat = false;
            collDialogue.enabled = false;

            barrier.SetActive(false);
            end.Invoke();
        }

        public void SetFirstArena()
        {
            BlockWayArena.firstArena = true;
        }
        public void SetSecondArena()
        {
            BlockWayArena.secondArena = true;
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