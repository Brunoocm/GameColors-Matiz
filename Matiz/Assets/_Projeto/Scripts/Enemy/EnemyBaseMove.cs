using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

namespace OniricoStudios
{

    [RequireComponent(typeof(NavMeshAgent))]
    [DefaultExecutionOrder(1)]
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
        [HideInInspector] public bool isStopped;

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

        //private SpriteRenderer spriteRenderer;
        private GameObject animated;
        private Animator anim;

        Vector3 size;

        GameObject targetObj;
        [HideInInspector] public NavMeshAgent navMeshAgent => gameObject.GetComponent<NavMeshAgent>();
        EnemyHealth enemyHealth => gameObject.GetComponent<EnemyHealth>();

        private void Awake()
        {
            EnemyMainAI.Instance.Units.Add(this);
            navMeshAgent.speed = speed;
            navMeshAgent.stoppingDistance = stopDistance;
            m_attackDuration = attackDuration;
        }

        void Start()
        {
            //targetObj = GameObject.FindGameObjectWithTag(tagNameTarget);

            anim = GetComponentInChildren<Animator>();
            animated = anim.gameObject;
            size = animated.transform.GetChild(0).localScale;
        }


        void Update()
        {

            if (!isStopped)
            {
                if (CharacterStats.playerObj != null)
                {
                    navMeshAgent.enabled = true;

                    seesTarget = Physics.CheckSphere(transform.position, EnemySeenRange, layerTarget);
                    attackingTarget = Physics.CheckSphere(transform.position, EnemyAttackRange, layerTarget);

                    //if (seesTarget) FollowTarget();
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
            else
            {
                Flip();
                navMeshAgent.enabled = false;
                
            }
        }
        public void MoveTo(Vector3 Position)
        {
            navMeshAgent.SetDestination(Position);
        }
        public void FollowTarget(Vector3 position)
        {
            if (!stopMoving && seesTarget)
            {
                Flip();
                navMeshAgent.SetDestination(position);
            }
            else
            {
                //navMeshAgent.ResetPath();
            }
        }

        void AttackTarget()
        {
            if (!enemyHealth.stopMove)
            {
                ResetMovement();
                eventAttack.Invoke();
                navMeshAgent.ResetPath();
            }
        }

        public void ResetMovement()
        {
            stopMoving = true;
            attackDuration = m_attackDuration;
            Flip();
            if(navMeshAgent.isActiveAndEnabled) navMeshAgent.ResetPath();
        }

        void Flip()
        {
            if (CharacterStats.playerObj.transform.position.x < transform.position.x)
            {
                animated.transform.GetChild(0).localScale = new Vector3(-size.x, size.y, size.z);
            }
            else
            {
                animated.transform.GetChild(0).localScale = new Vector3(size.x, size.y, size.z);
            }
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
