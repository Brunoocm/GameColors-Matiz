using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health;
    public float timeKnockback;
    public float forceKnockback;


    [SerializeField] private Material flashMaterial;

    [SerializeField] private float duration;

    private SpriteRenderer spriteRenderer;

    private Material originalMaterial;

    private Coroutine flashRoutine;

    GameObject playerObj;
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        originalMaterial = spriteRenderer.material;
    }

    void Update()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Flash()
    {
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        spriteRenderer.material = flashMaterial;

        yield return new WaitForSeconds(duration);

        spriteRenderer.material = originalMaterial;

        flashRoutine = null;
    }
    public void DamageVoid(int dano)
    {
        health -= dano;
        Flash();
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
