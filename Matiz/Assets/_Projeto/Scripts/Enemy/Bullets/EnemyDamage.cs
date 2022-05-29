using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class EnemyDamage : MonoBehaviour
    {
        public int damage;
        Transform pos;

        private void Awake()
        {
            pos = transform.parent;
        }
        void Start()
        {
            
        }

        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {

            if (other.gameObject.CompareTag("Player"))
            {
                if (other.GetComponent<CharacterStats>() != null)
                {
                    other.GetComponent<CharacterStats>().DamageVoid(damage, pos);
                }
            }
        }
    }
}
