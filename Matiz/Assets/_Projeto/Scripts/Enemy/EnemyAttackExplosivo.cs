using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackExplosivo : MonoBehaviour
{
    [Header("Stats")]
    public int damage;
    public float antecipation;

    [Header("Dash")]
    public float dashForce;
    public float timeDash;
    public GameObject bulletExplosion;

    [Header("Target")]
    public string tagNameTarget;
    private bool oneTime;
    GameObject targetObj;

    Rigidbody rb => gameObject.GetComponent<Rigidbody>();
    Animator anim => gameObject.GetComponent<Animator>();
    void Start()
    {
        targetObj = GameObject.FindGameObjectWithTag(tagNameTarget);
    }

    void Update()
    {
    }

    public void AttackVoid()
    {
        if (!oneTime)
        {
            //anim.SetTrigger("AntecipationTrigger");
            StartCoroutine(Dash(targetObj.transform));
        }
    }

    public void HitTarget(GameObject target)
    {
       
        if (targetObj.gameObject.GetComponent<CharacterStats>())
        {
            targetObj.gameObject.GetComponent<CharacterStats>().DamageVoid(damage);
        }
    }

    public void Explosion()
    {
        GameObject bullet = bulletExplosion;
        bullet.GetComponent<ExplosionBullet>().damage = damage;
        Instantiate(bullet, transform.position, Quaternion.identity);

        Destroy(gameObject);

    }

    public IEnumerator Dash(Transform dir)
    {
        oneTime = true;

        yield return new WaitForSeconds(antecipation);
        //anim.SetTrigger("DashTrigger");

        float vertical = dir.position.z - transform.position.z;
        float horizontal = dir.position.x - transform.position.x;

        float startTime = Time.time;
        while (Time.time < startTime + timeDash)
        {
            transform.Translate(new Vector3(horizontal, 0, vertical).normalized * dashForce * Time.deltaTime);
            yield return null;
        }

        Explosion();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tagNameTarget))
        {
            HitTarget(other.gameObject);
            Explosion();


        }
    }


}
