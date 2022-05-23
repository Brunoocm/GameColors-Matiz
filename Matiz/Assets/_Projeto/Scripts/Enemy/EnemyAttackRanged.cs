using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAttackRanged : MonoBehaviour
{
    [Header("Stats")]
    public int damage;
    public float antecipation;
    public float fireAngle;

    [Header("Jump")]
    public float timeJump;
    public float speedJump;
    public float cdJump;

    [Header("Target")]
    public Transform muzzle;
    public GameObject bullet;
    public string tagNameTarget;
    private bool oneTime;

    void Start()
    {
        oneTime = false;
    }

    void Update()
    {
        
    }

    public void AttackVoid()
    {
        if (!oneTime)
        {
            int num = Random.Range(0, 4);
            StartCoroutine(Jump(num));
        }
    }

    public void Shoot()
    {
        float value = Random.Range(-fireAngle, fireAngle);
        var rot = muzzle.rotation;
        rot *= Quaternion.Euler(0, value, 0);
        Instantiate(bullet, muzzle.position, rot);
    }
    public IEnumerator Jump(int num)
    {
        oneTime = true;

        float startTime = Time.time;
        while (Time.time < startTime + timeJump)
        {
            if (num == 0) transform.Translate(Vector3.forward * speedJump * Time.deltaTime);
            if (num == 1) transform.Translate(Vector3.back * speedJump * Time.deltaTime);
            if (num == 2) transform.Translate(Vector3.left * speedJump * Time.deltaTime);
            if (num == 3) transform.Translate(Vector3.right * speedJump * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(cdJump);
        Shoot();
        oneTime = false;

    }



}
