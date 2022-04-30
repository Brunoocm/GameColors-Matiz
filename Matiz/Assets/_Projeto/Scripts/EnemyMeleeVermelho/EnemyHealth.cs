using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [Header("Stats")]
    public int health;
    public float timeKnockback;
    public float forceKnockback;
    [HideInInspector]public bool stopMove;

    [Header("Stacks")]
    public int currentStacks;

    [SerializeField] private Material flashMaterial;

    [SerializeField] private float duration;

    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    private Coroutine flashRoutine;
    GameObject playerObj;

    CharacterAbilities characterAbilities => FindObjectOfType<CharacterAbilities>();
    EnemyBaseMove enemyBaseMove => GetComponent<EnemyBaseMove>();
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
        print(stopMove);
        if(stopMove)
        {
            enemyBaseMove.ResetMovement();
        }
        else
        {

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
        if(characterAbilities.cinzaTrue) //HABILIDADE CINZA Script: CharacterAbilities.cs
        {
            currentStacks++;

            if(currentStacks == characterAbilities.cinzaAbility.numStacks)
            {
                health -= dano * 2;
                currentStacks = 0;
            }
            else
            {
                health -= dano;

            }
        }                                //HABILIDADE CINZA Script: CharacterAbilities.cs
        else
        {
            health -= dano;  
        }

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






    public void SkillCinzaVoid(Transform target, float time, float force, int dano)
    {
        if (characterAbilities.cinzaTrue) //HABILIDADE CINZA Script: CharacterAbilities.cs
        {
            currentStacks++;

            if (currentStacks == characterAbilities.cinzaAbility.numStacks)
            {
                health -= dano * 2;
                currentStacks = 0;
            }
            else
            {
                health -= dano;

            }
        }                                //HABILIDADE CINZA Script: CharacterAbilities.cs
        else
        {
            health -= dano;
        }

        Flash();
        StartCoroutine(SkillCinza(target, time, force));
    }

    public IEnumerator SkillCinza(Transform target, float time, float force)
    {
        Vector3 finalPos = new Vector3(target.position.x - transform.position.x, 0, target.position.z - transform.position.z);

        float startTime = Time.time;
        while (Time.time < startTime + time)
        {
            transform.Translate(new Vector3(finalPos.x, 0, finalPos.z).normalized * force * Time.deltaTime);
            enemyBaseMove.stopMoving = true;
            yield return null;
        }

        stopMove = true;

        yield return new WaitForSeconds(2f);

        stopMove = false;

    }
}
