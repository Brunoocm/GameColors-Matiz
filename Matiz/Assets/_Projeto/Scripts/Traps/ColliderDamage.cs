using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class ColliderDamage : MonoBehaviour
    {
        public int damage;
        public bool lava;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<CharacterStats>() != null)
            {
                other.GetComponent<CharacterStats>().DamageVoid(damage, transform);
                if (lava)
                {
                    FMODUnity.RuntimeManager.PlayOneShot(AudioScript.Instance.LavaEvent, transform.position);
                }
            }

            //if (other.GetComponent<EnemyHealth>() != null)
            //{
            //    other.GetComponent<EnemyHealth>().DamageVoid(damage);
            //}
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<CharacterStats>() != null)
            {
                collision.gameObject.GetComponent<CharacterStats>().DamageVoid(damage, transform);
                if (lava)
                {
                    FMODUnity.RuntimeManager.PlayOneShot(AudioScript.Instance.LavaEvent, transform.position);
                }
            }

            //if (collision.gameObject.GetComponent<EnemyHealth>() != null)
            //{
            //    collision.gameObject.GetComponent<EnemyHealth>().DamageVoid(damage);
            //}
        }
    }
}