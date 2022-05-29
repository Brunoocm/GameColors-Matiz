using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class EnemyHealth : MonoBehaviour
    {
        [Header("Stats")]
        public int health;
        public float timeKnockback;
        public float forceKnockback;
        [HideInInspector] public bool stopMove;

        public GameObject greyPassive;

        public GameObject hurtFX;
        public GameObject deathFX;

        [Header("Stacks")]
        public int currentStacks;
        float stackTime;

        [SerializeField] private Material flashMaterial;

        [SerializeField] private float duration;

        float invulnerable;
        bool canDamage;

        private SpriteRenderer spriteRenderer;
        private Material originalMaterial;
        private Coroutine flashRoutine;
        //GameObject playerObj;

        CharacterAbilities characterAbilities => FindObjectOfType<CharacterAbilities>();
        EnemyBaseMove enemyBaseMove => GetComponent<EnemyBaseMove>();
        EnemyMainAI enemyMainAI => FindObjectOfType<EnemyMainAI>();

        TimerArena timerArena;
        void Start()
        {
            if(FindObjectOfType<TimerArena>())
            {
                timerArena = FindObjectOfType<TimerArena>();
            }

            //playerObj = GameObject.FindGameObjectWithTag("Player");

            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            originalMaterial = spriteRenderer.material;

            GreyPassiveSkill();
        }

        void Update()
        {
            if (health <= 0)
            {
                enemyMainAI.DeleteObj(gameObject, enemyBaseMove);
                Instantiate(deathFX);

                //transform.gameObject.SetActive(false);


                Destroy(gameObject);
            }

            if (stopMove)
            {
                enemyBaseMove.ResetMovement();
            }

            if(invulnerable > 0)
            {
                invulnerable -= Time.deltaTime;
                canDamage = false;
            }
            else
            {
                canDamage = true;
            }

            if(stackTime > 0)
            {
                stackTime -= Time.deltaTime;
            }
            else
            {
                currentStacks = 0;

                GreyPassiveSkill();
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
            if (canDamage)
            {
                if (characterAbilities.cinzaTrue) //HABILIDADE CINZA Script: CharacterAbilities.cs
                {
                    if (currentStacks == characterAbilities.cinzaAbility.numStacks - 1)
                    {
                        health -= dano * 2;
                        currentStacks = 0;
                        GreyPassiveSkill();
                    }
                    else
                    {
                        health -= dano;
                        currentStacks++;
                        GreyPassiveSkill();
                    }
                }                                //HABILIDADE CINZA Script: CharacterAbilities.cs
                else
                {
                    health -= dano;
                }

                Instantiate(hurtFX);

                timerArena.AddTime();
                Flash();
                StartCoroutine(Knockback());
            }
        }

        public void GreyPassiveSkill()
        {
            stackTime = 5;

            if(currentStacks < characterAbilities.cinzaAbility.numStacks && currentStacks != 0)
            {
                for (int i = 0; i < currentStacks; i++)
                {
                    if (greyPassive.transform.GetChild(i) != null)
                    {
                        greyPassive.transform.GetChild(i).gameObject.SetActive(true);
                    }
                }

                greyPassive.transform.GetComponent<SpriteRenderer>().enabled = true;
            }
            else
            {
                for (int i = 0; i < greyPassive.transform.childCount; i++)
                {
                    greyPassive.transform.GetChild(i).gameObject.SetActive(false);
                }

                greyPassive.transform.GetComponent<SpriteRenderer>().enabled = false;
            }
        }

        public IEnumerator Knockback()
        {
            Vector3 finalPos = new Vector3(
                CharacterStats.playerObj.transform.position.x - transform.position.x,
                0, CharacterStats.playerObj.transform.position.z - transform.position.z);

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

                if (currentStacks == characterAbilities.cinzaAbility.numStacks - 1)
                {
                    health -= dano * 2;
                    GreyPassiveSkill();
                    currentStacks = 0;
                }
                else
                {
                    health -= dano;
                    currentStacks++;
                    GreyPassiveSkill();
                }
            }                                //HABILIDADE CINZA Script: CharacterAbilities.cs
            else
            {
                health -= dano;
            }


            timerArena.AddTime();
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("AttackPlayer"))
            {
                invulnerable = 0.25f;
            }
        }
    }
}