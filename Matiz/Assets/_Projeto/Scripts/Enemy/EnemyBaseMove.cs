using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

namespace OniricoStudios
{
    public class EnemyBaseMove : MonoBehaviour
    {
        [Header("Stats")]
        [Range(0.0f, 50f)]
        public float EnemySeenRange;
        [Range(0.0f, 50f)]
        public float EnemyAttackRange;
        public float attackDuration;
        public float stopDistance;
        public float speed;

        [Header("Gizmos")]
        public bool gizmos;

        [Header("VisionEnemy")]
        public bool seesTarget;
        public bool attackingTarget;
        public bool stopMoving;
        public LayerMask layerTarget;
        public string tagNameTarget;

        [Header("Attacks")]
        public UnityEvent eventAttack;

        private float m_attackDuration;

        private SpriteRenderer spriteRenderer;
        private GameObject sprite;

        GameObject targetObj;
        NavMeshAgent navMeshAgent => gameObject.GetComponent<NavMeshAgent>();
        EnemyHealth enemyHealth => gameObject.GetComponent<EnemyHealth>();

        private void Awake()
        {
            spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
            sprite = spriteRenderer.gameObject;
            navMeshAgent.speed = speed;
            navMeshAgent.stoppingDistance = stopDistance;
            m_attackDuration = attackDuration;
        }

        void Start()
        {
            //targetObj = GameObject.FindGameObjectWithTag(tagNameTarget);
        }


        void Update()
        {
            

            if (CharacterStats.playerObj != null)
            {
                seesTarget = Physics.CheckSphere(transform.position, EnemySeenRange, layerTarget);
                attackingTarget = Physics.CheckSphere(transform.position, EnemyAttackRange, layerTarget);

                if (seesTarget) FollowTarget();
                if (attackingTarget) AttackTarget();

                if (stopMoving)
                {
                    attackDuration -= Time.deltaTime;
                    if (attackDuration <= 0)
                    {
                        stopMoving = false;
                        //attackDuration = m_attackDuration;
                    }
                }
            }
        }

        void FollowTarget()
        {
            if (!stopMoving)
            {
                Vector3 target = new Vector3(CharacterStats.playerObj.transform.position.x, transform.position.y, CharacterStats.playerObj.transform.position.z);
                navMeshAgent.SetDestination(target);
                Flip();
            }
            else
            {
                navMeshAgent.ResetPath();
            }
        }

        void AttackTarget()
        {
            if (!enemyHealth.stopMove)
            {
                ResetMovement();
                eventAttack.Invoke();
            }
        }

        public void ResetMovement()
        {
            stopMoving = true;
            attackDuration = m_attackDuration;
            Flip();
            navMeshAgent.ResetPath();
        }

        void Flip()
        {
            if(CharacterStats.playerObj.transform.position.x < transform.position.x)
            {
                sprite.transform.localRotation = Quaternion.Euler(-45, 180, 0);
            }
            else
            {
                sprite.transform.localRotation = Quaternion.Euler(45, 0, 0);

            }

            //spriteRenderer.sprite.transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);

        }
        void OnDrawGizmosSelected()
        {
            if (gizmos)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(transform.position, EnemySeenRange);

                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(transform.position, EnemyAttackRange);
            }
        }


    }
}
