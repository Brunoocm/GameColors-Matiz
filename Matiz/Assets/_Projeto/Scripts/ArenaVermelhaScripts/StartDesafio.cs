using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace OniricoStudios
{


    public class StartDesafio : MonoBehaviour
    {
        public GameObject enemy;
        public GameObject canvas;

        public UnityEvent start;
        public UnityEvent end;

        private GameObject currentEnemy;

        private bool inCombat;
        private bool isFinished;
        void Start()
        {

        }

        void Update()
        {
            if (inCombat && currentEnemy.activeSelf && !isFinished)
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
            yield return new WaitForSeconds(3);

            currentEnemy = Instantiate(enemy, transform.position, Quaternion.identity);

            start.Invoke();

            inCombat = true;
        }

        public void EndDesavio()
        {
            isFinished = true;
            inCombat = false;
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