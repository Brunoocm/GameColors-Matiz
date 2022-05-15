using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OniricoStudios;
public class FollowPlayer : MonoBehaviour
{
    public bool follow;

    GameObject player;
    Vector3 pos;
    void Start()
    {
        player = FindObjectOfType<CharacterStats>().gameObject;
        pos = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
    }

    void Update()
    {
        if(follow)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 1.3f);
        }
    }
}
