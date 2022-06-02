using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
namespace OniricoStudios
{
    public class EnemyAttackLancador : MonoBehaviour
    {
        public GameObject enemyExplosivo;
        public float cooldownShoot;

        private bool oneTime;

        NavMeshAgent navMeshAgent => gameObject.GetComponent<NavMeshAgent>();
        Animator anim => gameObject.GetComponentInChildren<Animator>();

        public void AttackVoid()
        {
            if (!oneTime)
            {
                StartCoroutine(Shoot());
            }
        }


        IEnumerator Shoot()
        {
            oneTime = true;

            anim.SetTrigger("attack");

            GameObject enemy = Instantiate(enemyExplosivo, transform.position, Quaternion.identity);
            enemy.GetComponent<EnemyAttackExplosivo>().launched = true;

            //enemy.GetComponent<EnemyAttackExplosivo>().AttackVoid();

            //enemy.GetComponent<EnemyAttackExplosivo>().launched = true;
            //enemy.GetComponent<EnemyAttackExplosivo>().AttackVoid();

            yield return new WaitForSeconds(cooldownShoot);

            oneTime = false;

        }


    }
}
