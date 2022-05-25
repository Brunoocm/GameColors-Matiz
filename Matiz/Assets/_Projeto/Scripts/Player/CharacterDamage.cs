using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class CharacterDamage : MonoBehaviour
    {
        CharacterStats charStats => gameObject.GetComponentInParent<CharacterStats>();

        IEnumerator DealDamage(GameObject enemy)
        {
            Camera.main.GetComponent<CameraScript>().Shake(500, 300);

            enemy.gameObject.GetComponent<EnemyHealth>().DamageVoid(charStats.damage);

            Instantiate(charStats.attackFX, enemy.transform.position, Quaternion.identity);

            Time.timeScale = 0.2f;

            yield return new WaitForSeconds(0.01f);

            Time.timeScale = 1;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy") && other.gameObject.GetComponent<EnemyHealth>() != null)
            {
                StartCoroutine(DealDamage(other.gameObject));
            }
        }
    }
}
