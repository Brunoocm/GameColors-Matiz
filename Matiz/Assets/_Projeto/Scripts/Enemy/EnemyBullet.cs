using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class EnemyBullet : MonoBehaviour
    {

        public int damage;
        public float speed;
        //public GameObject vfx;

        GameObject target;

        Rigidbody rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();

            target = GameObject.FindGameObjectWithTag("Player");

            float value = Random.Range(-11, 11);
            
            Destroy(gameObject, 5f);
        }

        private void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (other.gameObject.GetComponent<CharacterStats>().canDamage)
                {
                    other.gameObject.GetComponent<CharacterStats>().DamageVoid(damage, transform);
                    Destroy(gameObject);
                }
            }
        }
    }
}