using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    public bool rangeVisible;
    public float rangeVision;
    public float rangeAttack;

    public float speed;

    public LayerMask playerLayer;

    public GameObject playerObj;
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {

        if (Physics.CheckSphere(transform.position, rangeVision, playerLayer))
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, playerObj.transform.position, speed * Time.deltaTime);
    }

    void OnDrawGizmosSelected()
    {
        if (rangeVisible)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, rangeAttack); 
            
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, rangeVision);
        }
    }
}
