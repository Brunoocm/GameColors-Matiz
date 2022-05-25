using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class EnemyDamage : MonoBehaviour
    {
        public int damage;

        void Start()
        {

        }

        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            print(other.name);

            if (other.gameObject.CompareTag("Enemy"))
            {
                if (other.GetComponent<EnemyHealth>() != null)
                {
                    other.GetComponent<EnemyHealth>().DamageVoid(damage);
                }
            }
        }
    }
}
