using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class BlockWayArena : MonoBehaviour
    {
        public GameObject firstArenaBlock;
        public GameObject secondArenaBlock;
        public GameObject thirdArenaBlock;
        public GameObject tutorial;
        public StartDesafio[] Battles;

        public static bool firstArena;
        public static bool secondArena;
        public static bool thirdArena;

        public static bool completeInsignia;

        ProgressionManager progressionManager => FindObjectOfType<ProgressionManager>();
        void Start()
        {
            Battles = FindObjectsOfType<StartDesafio>();
        }

        void Update()
        {
            InsigniaTrue();

            if (progressionManager.unlockFirstArena)
            {
                firstArenaBlock.SetActive(false);
            }
            if(progressionManager.unlockSecondArena)
            {
                print("oo");
                secondArenaBlock.SetActive(false);
            }
            if(progressionManager.unlockFinal)
            {
                print("oo");
                thirdArenaBlock.SetActive(false);
            }
            if(progressionManager.tutorial)
            {
                tutorial.SetActive(false);
            }
           

            if (firstArena)
            {
                firstArenaBlock.SetActive(false);
            }
            if (secondArena)
            {
                secondArenaBlock.SetActive(false);

            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
            
            }
        }
        private void OnCollisionEnter(Collision other)
        {
         
        }

        public void InsigniaTrue()
        {
            if(Battles[0].isFinished && Battles[1].isFinished && Battles[2].isFinished)
            {
                progressionManager.unlockFirstArena = true;
            }
        }
    }
}
