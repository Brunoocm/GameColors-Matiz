using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OniricoStudios
{
    public class WavesArena : MonoBehaviour
    {
        public GameObject[] waves;
        public GameObject countWaves;

        private bool hasEnemy;
        private bool started;
        private int num;
        void Start()
        {
        }

        void Update()
        {
            hasEnemy = FindObjectOfType<EnemyHealth>();

            if (!hasEnemy)
            {
                if (num < waves.Length)
                {
                    waves[num].gameObject.SetActive(true);
                    if (started)
                    {
                        countWaves.transform.GetChild(num - 1).gameObject.SetActive(false);

                    }
                    started = true;
                    num++;
                }
                else if (num == waves.Length)
                {
                    countWaves.transform.GetChild(num - 1).gameObject.SetActive(false);

                }
                else if (num == 4)
                {

                }
            }


        }

        public void CountWaves()
        {

        }
    }
}