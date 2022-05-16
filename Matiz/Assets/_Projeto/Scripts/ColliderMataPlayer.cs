using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class ColliderMataPlayer : MonoBehaviour
    {
        MainCheckpoint mainCheckpoint => FindObjectOfType<MainCheckpoint>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<CharacterStats>() != null)
            {

                if (!other.gameObject.GetComponent<CharacterStats>().isDead)
                {
                    print("aa");
                    mainCheckpoint.Death();
                    other.gameObject.GetComponent<CharacterStats>().isDead = true;
                }
            }
        }
    }
}
