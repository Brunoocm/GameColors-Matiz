using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public float timeKnockback;
    public float forceKnockback;

    GameObject playerObj;
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
    }

    public void DamageVoid(int dano)
    {
        health -= dano;
        StartCoroutine(Knockback());
    }

    public IEnumerator Knockback()
    {
        Vector3 finalPos = new Vector3(playerObj.transform.position.x - transform.position.x, 0, playerObj.transform.position.z - transform.position.z);

        float startTime = Time.time;
        while (Time.time < startTime + timeKnockback)
        {
            transform.Translate(new Vector3(-finalPos.x, 0, -finalPos.z).normalized * forceKnockback * Time.deltaTime);
            yield return null;
        }

    }
}
