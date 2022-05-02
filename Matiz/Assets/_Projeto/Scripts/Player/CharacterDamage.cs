using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class CharacterDamage : MonoBehaviour
    {
        CharacterStats charStats => gameObject.GetComponentInParent<CharacterStats>();

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<EnemyHealth>().DamageVoid(charStats.damage);

                Instantiate(charStats.attackFX, other.transform.position, Quaternion.identity);
            }
        }
    }
}
