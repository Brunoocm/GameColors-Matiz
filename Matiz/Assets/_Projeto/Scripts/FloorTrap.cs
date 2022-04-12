using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTrap : MonoBehaviour
{
    bool trapActive;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy")) && anim != null && !trapActive)
        {
            anim.SetTrigger("ativa");
            trapActive = true;
        }
    }

    public void DeactivateTrap()
    {
        trapActive = false;
    }
}
