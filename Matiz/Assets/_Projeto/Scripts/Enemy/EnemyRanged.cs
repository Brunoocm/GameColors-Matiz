using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    public float retreatRange;
    public float fireRange;
    public float speed;
    public LayerMask playerLayer;

    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        //
    }

    void Retreat()
    {
        //
    }
}
