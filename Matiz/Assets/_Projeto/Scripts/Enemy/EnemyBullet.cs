using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    //public variables

    public int damage;
    public float speed;
    //public GameObject vfx;

    //private variables

    GameObject target;

    //components

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        target = GameObject.FindGameObjectWithTag("Player");

        //transform.eulerAngles = Vector3.zero;
    }

    private void Update()
    {
        //transform.Translate(target.transform.position * speed * Time.deltaTime);
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
