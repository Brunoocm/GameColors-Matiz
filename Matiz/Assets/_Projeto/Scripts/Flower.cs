using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Flower : MonoBehaviour
{
    public GameObject lifePoints;
    public int maxLifePoints;
    public float range;
    public float timeToRespawn;

    SpriteRenderer spriteRenderer => GetComponentInChildren<SpriteRenderer>();
    BoxCollider col => GetComponent<BoxCollider>();
    void Start()
    {
        
    }

    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("AttackPlayer"))
        {

            int num = Random.Range(1, maxLifePoints);
            int i = 0;
            while (i <= num)
            {
                StartCoroutine(SpawnObj());
                i++;
            }

            StartCoroutine(Respawn());
        }
    }

    IEnumerator SpawnObj()
    {
        Vector3 currentPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        float random = Random.Range(-range, range/2);
        float random2 = Random.Range(-range, range/2);

        GameObject obj = Instantiate(lifePoints, transform.position, Quaternion.identity);
        obj.GetComponent<Collider>().enabled = false;
        obj.transform.DOLocalMove(new Vector3(currentPos.x + random, currentPos.y, currentPos.z + random2), 0.5f);

        yield return new WaitForSeconds(0.5f);

        obj.GetComponent<FollowPlayer>().follow = true;
        obj.GetComponent<Collider>().enabled = true;


    }
    IEnumerator Respawn()
    {
        spriteRenderer.enabled = false;
        col.enabled = false;

        yield return new WaitForSeconds(timeToRespawn);

        spriteRenderer.enabled = true;
        col.enabled = true;

    }
}
