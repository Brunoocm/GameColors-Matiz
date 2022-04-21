using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCinza : MonoBehaviour
{
    public int damage;
    public float timeKnockback;
    public float forceKnockback;
    void Start()
    {
        
    }

    void Update()
    {
    }

    public IEnumerator Knockback(Collider target)
    {
        Vector3 finalPos = new Vector3(transform.position.x - target.transform.position.x, 0, transform.position.z - target.transform.position.z);

        float startTime = Time.time;
        while (Time.time < startTime + timeKnockback)
        {
            target.transform.Translate(new Vector3(finalPos.x, 0, finalPos.z).normalized * forceKnockback * Time.deltaTime);
            yield return null;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(Knockback(other));
            other.GetComponent<EnemyHealth>().DamageVoid(damage);
        }
    }
}
