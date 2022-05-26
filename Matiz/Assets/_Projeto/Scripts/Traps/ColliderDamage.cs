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
            print(other.name);

            if (other.gameObject.CompareTag("Player"))
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

        private void OnCollisionEnter(Collision collision)
        {
            print(collision.gameObject.name);

            if (collision.gameObject.CompareTag("Player"))
            {
                if (collision.gameObject.GetComponent<CharacterStats>() != null)
                {
                    collision.gameObject.GetComponent<CharacterStats>().DamageVoid(damage, transform);
                }
                else if (collision.gameObject.GetComponent<EnemyHealth>() != null)
                {
                    collision.gameObject.GetComponent<EnemyHealth>().DamageVoid(damage);
                }
            }
        }
    }
}