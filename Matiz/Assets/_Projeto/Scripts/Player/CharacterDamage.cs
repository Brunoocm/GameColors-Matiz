using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamage : MonoBehaviour
{
    CharacterStats charStats => gameObject.GetComponentInParent<CharacterStats>();

    void Start()
    {
      
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyHealth>().DamageVoid(charStats.damage);

        }

    }
}
