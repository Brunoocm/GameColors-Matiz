using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OniricoStudios
{
    public class ArrowTrap : MonoBehaviour
    {
        public float cooldown;
        public bool doubleShot;
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

            if (doubleShot)
            {
                StartCoroutine(ShootAgain());
            }
        }

        IEnumerator ShootAgain()
        {
            yield return new WaitForSeconds(0.25f);

            Instantiate(arrow, firePoint.position, firePoint.rotation);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.GetComponent<CharacterStats>() != null && canShoot)
            {
                ShootArrow();
                FMODUnity.RuntimeManager.PlayOneShot(AudioScript.Instance.ArrowsEvent, transform.position);
            }
        }
    }
}
