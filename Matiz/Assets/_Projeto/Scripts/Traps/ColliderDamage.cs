using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class ColliderDamage : MonoBehaviour
    {
        public int damage;
        public float lavaCooldown;
        public bool lava;

        private float cooltime;
        private bool onLava;
        private GameObject player;

        private void Update()
        {
            if (lava)
            {
                if (cooltime <= 0)
                {
                    player.GetComponent<CharacterStats>().DamageVoid(damage, transform);
                    cooltime = lavaCooldown;
                }
                else
                {
                    cooltime -= Time.deltaTime;
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<CharacterStats>() != null && !lava)
            {
                other.GetComponent<CharacterStats>().DamageVoid(damage, transform);
            }
            else if(other.GetComponent<CharacterStats>() != null && lava)
            {
                player = other.gameObject;

                FMODUnity.RuntimeManager.PlayOneShot(AudioScript.Instance.LavaEvent, transform.position);
                onLava = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<CharacterStats>() != null)
            {
                onLava = false;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<CharacterStats>() != null && !lava)
            {
                collision.gameObject.GetComponent<CharacterStats>().DamageVoid(damage, transform);
            }
            else if (collision.gameObject.GetComponent<CharacterStats>() != null && lava)
            {
                player = collision.gameObject.gameObject;

                FMODUnity.RuntimeManager.PlayOneShot(AudioScript.Instance.LavaEvent, transform.position);
                onLava = true;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.GetComponent<CharacterStats>() != null)
            {
                onLava = false;
            }
        }
    }
}