using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OniricoStudios
{
    public class ArrowTrap : MonoBehaviour
    {
        public float cooldown;
        public GameObject arrow;
        public Transform firePoint;

        float cooltime;
        bool canShoot;

        private void Update()
        {
            if (cooltime <= 0)
            {
                canShoot = true;
            }
            else
            {
                cooltime -= Time.deltaTime;
                canShoot = false;
            }
        }

        void ShootArrow()
        {
            Instantiate(arrow, firePoint.position, firePoint.rotation);
            cooltime = cooldown;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<CharacterStats>() != null && canShoot)
            {
                ShootArrow();
            }
        }
    }
}
