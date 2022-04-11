using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    //public variables

    public float followSpeed;
    public float followRange;
    public float retreatSpeed;
    public float retreatRange;
    public LayerMask playerLayer;

    //private variables

    GameObject player;

    //components

    Animator anim;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(Physics.CheckSphere(transform.position, retreatRange, playerLayer))
        {
            FollowRetreat(-retreatSpeed);
        }
        else if(Physics.CheckSphere(transform.position, followRange, playerLayer))
        {
            FollowRetreat(followSpeed);
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
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        
        if(anim != null)
        {
            anim.SetBool("walk", true);
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
