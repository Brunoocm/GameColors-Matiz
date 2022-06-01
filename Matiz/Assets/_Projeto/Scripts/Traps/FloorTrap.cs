using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    
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
        if ((other.gameObject.CompareTag("Player") && anim != null && !trapActive))
        {
            anim.SetTrigger("ativa");
            trapActive = true;
            FMODUnity.RuntimeManager.PlayOneShot(AudioScript.Instance.SpearsEvent, transform.position);
        }
    }

    public void DeactivateTrap()
    {
        trapActive = false;
    }
}
}
