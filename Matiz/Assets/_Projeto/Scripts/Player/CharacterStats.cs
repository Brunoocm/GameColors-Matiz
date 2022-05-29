using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;


namespace OniricoStudios
{
    public class CharacterStats : MonoBehaviour
    {
        [Header("stats")]
        public int health;
        [HideInInspector] public int m_health;
        [HideInInspector] public int maxHealth = 3;
        public int damage;
        [HideInInspector] public int m_damage;

        public float timeInvencible;
        [HideInInspector] public float m_timeInvencible;

        [HideInInspector] public bool canUseSkill;
        [HideInInspector] public bool isDead;
        public SpriteRenderer sprite;
        public GameObject attackFX;
        public GameObject damageFX;
        public GameObject prismFX;
        public GameObject deathFX;
        public Transform vfxPivot;

        //GameObject bloodyScreen;


        [Header("Shield")]
        public bool hasShield;
        public bool canDamage;
        public GameObject dropPrisma;
        public GameObject shieldObj;
        private GameObject m_shieldObj;
        public GameObject prismCompass;

        public static CharacterStats playerObj;

        public static bool cinzaTrue;
        public static bool vermelhoTrue;
        public static bool azulTrue;
        public static bool verdeTrue;

        CharacterMovement charMove => GetComponent<CharacterMovement>();
        CharacterAttack charAttack => GetComponent<CharacterAttack>();
        CharacterAbilities charAbilities => GetComponent<CharacterAbilities>();
        MainCheckpoint mainCheckpoint => FindObjectOfType<MainCheckpoint>();
        CinemachineVirtualCamera cinemachineVirtualCamera => FindObjectOfType<CinemachineVirtualCamera>();
        ExtraLifeScript extraLifeScript => FindObjectOfType<ExtraLifeScript>();
        

        private void Awake()
        {
            playerObj = this;

            //shieldObj = GameObject.FindGameObjectWithTag("PrismaFollow");
            if(GameObject.FindGameObjectWithTag("PrismaFollow") == null)
            {
                m_shieldObj = Instantiate(shieldObj, transform.position, Quaternion.identity);
            }
            else
            {
                Destroy(GameObject.FindGameObjectWithTag("PrismaFollow"));
                m_shieldObj = Instantiate(shieldObj, transform.position, Quaternion.identity);
            }
        }
        void Start()
        {
            hasShield = true;
            canDamage = true;
            m_health = health;
            m_damage = damage;
            m_timeInvencible = timeInvencible;
            timeInvencible = 0;
            cinemachineVirtualCamera.Follow = this.transform;
            //cinemachineVirtualCamera.LookAt = this.transform;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                DamageVoid(1, transform);
            }

            if (timeInvencible <= 0)
            {
                canDamage = true;
            }
            else
            {
                timeInvencible -= Time.deltaTime;

            }

            if (health <= 0)
            {
                if (!isDead)
                {
                    Die();
                }
            }

            if (canDamage)
            {
                sprite.DOColor(Color.white, 1);

                //sprite.color = Color.white;
            }
            else
            {
                //sprite.DOColor(Color.black, 0.3f).SetLoops(-1, LoopType.Yoyo);

            }

        

        }

        void Die()
        {
            Instantiate(deathFX, vfxPivot.position, Quaternion.identity);

            mainCheckpoint.Death();
            isDead = true;
        }

        public void DamageVoid(int dano, Transform enemy)
        {
            if (canDamage)
            {
                if (hasShield)
                {
                    float enemyX = transform.position.x - enemy.position.x;
                    float enemyZ = transform.position.z - enemy.position.z;

                    charAttack.SprintLR(enemyX, enemyZ);


                    SpawnPrisma();
                    m_shieldObj.SetActive(false);        
                    hasShield = false;
                    prismCompass.SetActive(true);
                    //Instantiate(prismFX, vfxPivot.position, Quaternion.identity);
                }
                else
                {
                    //piscadinha branca

                    float enemyX = transform.position.x - enemy.position.x;
                    float enemyZ = transform.position.z - enemy.position.z;

                    charAttack.SprintLR(enemyX, enemyZ);

                    health -= dano;
                    Instantiate(damageFX, vfxPivot.position, Quaternion.identity);
                }

                timeInvencible = m_timeInvencible;
                InvulnerableTime();

                canDamage = false;
            }
        }

        public void GiveShield()
        {
            m_shieldObj.SetActive(true);
            hasShield = true;
            prismCompass.SetActive(false);
        }

        public void SpawnPrisma()
        {
            Vector3 pos = new Vector3(m_shieldObj.transform.position.x, m_shieldObj.transform.position.y + 5, m_shieldObj.transform.position.z);

            GameObject prisma = Instantiate(dropPrisma, pos, Quaternion.identity);
            prisma.GetComponent<PrismaBase>().characterStats = gameObject.GetComponent<CharacterStats>();

        }

        private void InvulnerableTime()
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(sprite.DOColor(Color.grey, 0.2f))
                .Append(sprite.DOColor(Color.white, 0.2f));
            mySequence.SetLoops(-1, LoopType.Restart);

        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("LifeOrb"))
            {
                extraLifeScript.currentStacksLife++;
                Destroy(other.gameObject);
            }
        }

    }
}