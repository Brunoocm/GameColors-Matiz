using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamage : MonoBehaviour
{
    public int damage;

    //CharacterAbilities characterAbilities => gameObject.GetComponentInParent<CharacterAbilities>();
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
            other.gameObject.GetComponent<EnemyHealth>().DamageVoid(damage);

        }

    }
}
