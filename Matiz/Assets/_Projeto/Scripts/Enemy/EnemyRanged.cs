using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    //public variables

    public float fireRange;
    public float fireRate;
    public LayerMask playerLayer;
    public GameObject bullet;
    //public GameObject vfx;
    public Transform muzzle;

    //private variables

    float fireTime;
    GameObject player;

    //components

    //Animator anim;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        //anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Physics.CheckSphere(transform.position, fireRange, playerLayer))
        {
            if(fireTime <= 0)
            {
                Shoot();
            }
            else
            {
                fireTime -= Time.deltaTime;
            }
        }
    }

    void Shoot()
    {
        Instantiate(bullet, muzzle.position, Quaternion.identity);
        //Instantiate(bullet, muzzle.position, muzzle.rotation);
        //Instantiate(vfx, muzzle.position, Quaternion.identity);
        fireTime = fireRate;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, fireRange);
    }
}
