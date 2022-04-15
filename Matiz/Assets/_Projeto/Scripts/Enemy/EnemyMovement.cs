using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float followSpeed;
    public float followRange;
    public float rangeJumpAttack;
    //public float retreatSpeed;
    //public float retreatRange;
    public LayerMask playerLayer;
    public LayerMask enemyLayer;


    [Header("RandomJump")]
    public float timeJump;
    public float speedJump;
    public Transform[] waypoints;
    private float m_timeJump;
    private int m_CurrentWaypointIndex;
    private bool isJumpAttack;


    [Header("aa")]
    private bool playerFound;
    private bool otherEnemyFound;

    public GameObject[] EnemyTransform;
    public Transform closestTarget;
    GameObject player;
    GameObject myObject;


    Animator anim => gameObject.GetComponent<Animator>();
    NavMeshAgent navMeshAgent => gameObject.GetComponent<NavMeshAgent>();
    private void Awake()
    {
        navMeshAgent.speed = followSpeed;
    }
    private void Start()
    {
        myObject = this.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");
        m_timeJump = timeJump;
    }

    private void Update()
    {
        playerFound = Physics.CheckSphere(transform.position, followRange, playerLayer);
        otherEnemyFound = Physics.CheckSphere(transform.position, followRange, enemyLayer);
        isJumpAttack = Physics.CheckSphere(transform.position, rangeJumpAttack, playerLayer);

        if(isJumpAttack)
        {
            JumpAttack();
        }
        else if (playerFound)
        {
            FollowRetreat(followSpeed); //Chase player


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

        if(!isJumpAttack)
        {
            Vector3 target = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
            navMeshAgent.SetDestination(target);
        }

        if (anim != null)
        {
            anim.SetBool("walk", true);
        }
    }

    

    private void JumpAttack()
    {
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);

        if (timeJump <= 0)
        {
            NextPoint();
            Move(followSpeed);
            m_CurrentWaypointIndex = Random.Range(0,3);
            //anim.SetTrigger("TriggerJump");
            resetTimerAnim(); //tirar dps
        }
        else
        {
            Move(followSpeed);
            timeJump -= Time.deltaTime;
        }


    }
    public void resetTimerAnim()
    {
        timeJump = m_timeJump;

    }

    public void NextPoint()
    {
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }
    //void Stop()
    //{
    //    navMeshAgent.isStopped = true;
    //    navMeshAgent.ResetPath();
    //    navMeshAgent.speed = 0;
    //}
    void Move(float speed)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.speed = speed;
    }
    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, followRange);

    //    Gizmos.color = Color.blue;
    //    Gizmos.DrawWireSphere(transform.position, rangeJumpAttack);
    //}

    //TESTE     //TESTE    //TESTE    //TESTE    //TESTE    //TESTE

    //void MoveAway(float speed)
    //{
    //    Vector3 playerTarget = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
    //    Vector3 closestEnemyTarget = new Vector3(closestTarget.transform.position.x, transform.position.y, closestTarget.transform.position.z);
    //    Vector3 myPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);


    //    navMeshAgent.SetDestination(closestEnemyTarget - myPos);

    //}
    ////TESTE    //TESTE    //TESTE    //TESTE    //TESTE    //TESTE

    //void CheckIsActive()
    //{

    //    float closestDistanceSqr = Mathf.Infinity;
    //    Vector3 currentPosition = transform.position;

    //    foreach (GameObject pointsTarget in EnemyTransform)
    //    {
    //        Vector3 directionToTarget = pointsTarget.transform.position - currentPosition;
    //        float dSqrToTarget = directionToTarget.sqrMagnitude;
    //        if (dSqrToTarget < closestDistanceSqr)
    //        {
    //            //for (int i = 0; i < EnemyTransform.Length; i++)
    //            //{
    //            //    pointsTarget++;
    //            //}

    //            if (pointsTarget == myObject)
    //                continue;
    //            closestDistanceSqr = dSqrToTarget;
    //            closestTarget = pointsTarget.transform;

    //        }
    //        else
    //        {

    //        }


    //    }

    //}
    //TESTE    //TESTE    //TESTE    //TESTE    //TESTE    //TESTE

}
