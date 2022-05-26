using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class EnemyAttackExplosivo : MonoBehaviour
    {
        [Header("Stats")]
        public int damage;
        public float explosionRange;
        public float antecipation;
        public float timeToExplode;
        [HideInInspector] public bool launched;

        [Header("Dash")]
        public float dashForce;
        public float timeDash;
        public GameObject bulletExplosion;

        [Header("Target")]
        public string tagNameTarget;
        private bool oneTime;
        private bool hitBy;
        GameObject targetObj;

        EnemyBaseMove enemyBaseMove => gameObject.GetComponent<EnemyBaseMove>();
        Rigidbody rb => gameObject.GetComponent<Rigidbody>();
        Animator anim => gameObject.GetComponent<Animator>();
        EnemyMainAI enemyMainAI => FindObjectOfType<EnemyMainAI>();
        void Start()
        {
            targetObj = GameObject.FindGameObjectWithTag(tagNameTarget);
        }

        void Update()
        {

        }

        public void AttackVoid()
        {
            if (!oneTime)
            {
                StartCoroutine(Dash(targetObj.transform));
            }
        }

        public void HitTarget(GameObject target)
        {

            if (targetObj.gameObject.GetComponent<CharacterStats>())
            {
                targetObj.gameObject.GetComponent<CharacterStats>().DamageVoid(damage, transform);
            }
        }

        public IEnumerator Explosion(float time)
        {
            yield return new WaitForSeconds(time);

            Explode();

            //GameObject bullet = bulletExplosion;
            //bullet.GetComponent<ExplosionBullet>().damage = damage;
            Instantiate(bulletExplosion, transform.position, Quaternion.identity);

            //Destroy(gameObject);

        }

        void Explode()
        {
            Collider[] hitExplosion = Physics.OverlapSphere(transform.position, explosionRange);

            foreach (Collider thing in hitExplosion)
            {
                if (thing.GetComponent<CharacterStats>() != null)
                {
                    thing.GetComponent<CharacterStats>().DamageVoid(damage, transform);
                }

                if (thing.GetComponent<EnemyHealth>() != null)
                {
                    thing.GetComponent<EnemyHealth>().DamageVoid(damage);
                }
            }

            enemyMainAI.DeleteObj(gameObject, enemyBaseMove);
            Destroy(gameObject);
        }

        public IEnumerator Dash(Transform dir)
        {
            oneTime = true;

            yield return new WaitForSeconds(antecipation);

            float vertical = dir.position.z - transform.position.z;
            float horizontal = dir.position.x - transform.position.x;

            float startTime = Time.time;
            while (Time.time < startTime + timeDash && !hitBy)
            {
                transform.Translate(new Vector3(horizontal, 0, vertical).normalized * dashForce * Time.deltaTime);
                yield return null;
            }

            if (!launched)
            {
                if (!hitBy) StartCoroutine(Explosion(0f));
            }
            else
            {
                oneTime = false;
                launched = false;
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(tagNameTarget))
            {
          
                HitTarget(other.gameObject);
                StartCoroutine(Explosion(0f));

            }
            if (other.gameObject.CompareTag("AttackPlayer"))
            {
                hitBy = true;
                if (hitBy)
                {
                    enemyBaseMove.stopMoving = true;
                    StartCoroutine(Explosion(timeToExplode));

                }
            }
        }


    }
}