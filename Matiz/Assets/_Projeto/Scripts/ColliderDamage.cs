using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class ColliderDamage : MonoBehaviour
    {
        public int damage;

        private void OnTriggerEnter(Collider other)
        {
            if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy")))
            {
                if (other.GetComponent<CharacterStats>() != null)
                {
                    other.GetComponent<CharacterStats>().DamageVoid(damage, transform);
                }
                else if (other.GetComponent<EnemyHealth>() != null)
                {
                    other.GetComponent<EnemyHealth>().DamageVoid(damage);
                }
            }
        }
    }
}