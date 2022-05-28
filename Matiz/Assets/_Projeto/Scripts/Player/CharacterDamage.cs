using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class CharacterDamage : MonoBehaviour
    {
        bool canDamage = true;

        CharacterStats charStats => gameObject.GetComponentInParent<CharacterStats>();

        CameraScript camScript => FindObjectOfType<CameraScript>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy") && other.gameObject.GetComponent<EnemyHealth>() != null)
            {
                print("========================================================================================================bruno");

                other.gameObject.GetComponent<EnemyHealth>().DamageVoid(charStats.damage);
            }
        }
    }
}
