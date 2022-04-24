using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialVermelho : MonoBehaviour
{
    public int damage;
    public float cooldown;
    public float timeToDestroy;

    private float time;

    private void Start()
    {
        Destroy(gameObject, timeToDestroy);
    }

    bool canDamage()
    {
        if(time <= cooldown)
        {
            time += Time.deltaTime;
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy") && other.GetComponent<EnemyHealth>() != null)
        {
            other.GetComponent<EnemyHealth>().DamageVoid(1);
            time = 0;
        }
    }
}
