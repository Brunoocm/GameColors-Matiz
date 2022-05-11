using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OniricoStudios
{
    public class ExplosionBullet : MonoBehaviour
    {
        public float timeToDisappear;
        public int damage;

        void Start()
        {
            Destroy(gameObject, timeToDisappear);
        }

        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<CharacterStats>().DamageVoid(damage, transform);
            }
        }
    }
}