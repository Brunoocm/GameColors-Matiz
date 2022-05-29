using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class BlockWayArena : MonoBehaviour
    {
        public GameObject firstArenaBlock;
        public GameObject secondArenaBlock;
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

            if (progressionManager.firstArena)
            {
                firstArenaBlock.SetActive(false);
            }
            if(progressionManager.secondArena)
            {
                secondArenaBlock.SetActive(false);
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
                progressionManager.firstArena = true;
            }
        }
    }
}
