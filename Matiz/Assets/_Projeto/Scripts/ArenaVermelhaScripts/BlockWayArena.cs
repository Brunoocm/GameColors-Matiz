using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class BlockWayArena : MonoBehaviour
    {
        public GameObject firstArenaBlock;
        public GameObject secondArenaBlock;

        public static bool firstArena;
        public static bool secondArena;
        public static bool thirdArena;
        void Start()
        {

        }

        void Update()
        {

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

        public void ArenaTrue()
        {

        }
    }
}
