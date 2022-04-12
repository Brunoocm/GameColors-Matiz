using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{

    public float followSpeed;
    public float followRange;
    public float retreatSpeed;
    public float retreatRange;
    public LayerMask playerLayer;
    public LayerMask enemyLayer;

    [Header("aa")]
    private bool playerFound;
    private bool otherEnemyFound;

    public GameObject[] EnemyTransform;
    public Transform closestTarget;
    GameObject player;
    GameObject myObject;

    Animator anim => gameObject.GetComponent<Animator>();
    NavMeshAgent navMeshAgent => gameObject.GetComponent<NavMeshAgent>();
    private void Start()
    {
        myObject = this.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        playerFound = Physics.CheckSphere(transform.position, followRange, playerLayer);
        otherEnemyFound = Physics.CheckSphere(transform.position, followRange, enemyLayer);

        if (playerFound)
        {
            FollowRetreat(followSpeed); //Chase player

          
        }
        if (otherEnemyFound)
        {
            EnemyTransform = GameObject.FindGameObjectsWithTag("Enemy");
            CheckIsActive();
            MoveAway(followSpeed);
        }
        else if (Physics.CheckSphere(transform.position, retreatRange, playerLayer))
        {
            //FollowRetreat(-retreatSpeed);
        }
        else if (Physics.CheckSphere(transform.position, followRange, enemyLayer))
        {
        }
        else
        {
            if (anim != null)
            {
                anim.SetBool("walk", false);
            }
        }
    }

    void FollowRetreat(float speed)
    {
        Vector3 target = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        navMeshAgent.SetDestination(target);

        if(anim != null)
        {
            anim.SetBool("walk", true);
        }
    }

    void MoveAway(float speed)
    {
        Vector3 target = new Vector3(closestTarget.transform.position.x, transform.position.y, closestTarget.transform.position.z);
        navMeshAgent.SetDestination(-target * 10);
    }

    void CheckIsActive()
    {

        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (GameObject pointsTarget in EnemyTransform)
        {
            Vector3 directionToTarget = pointsTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                //for (int i = 0; i < EnemyTransform.Length; i++)
                //{
                //    pointsTarget++;
                //}

                if (pointsTarget == myObject)
                    continue;
                closestDistanceSqr = dSqrToTarget;
                closestTarget = pointsTarget.transform;

            }
            else
            {

            }


        }

    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, retreatRange);
    }
}
