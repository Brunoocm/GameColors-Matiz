using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace OniricoStudios
{
    public class CharacterStats : MonoBehaviour
    {
        [Header("stats")]
        public int health;
        [HideInInspector] public int m_health;
        public int damage;
        [HideInInspector] public int m_damage;

        public float timeInvencible;
        [HideInInspector] public float m_timeInvencible;

        [HideInInspector] public bool canUseSkill;
        [HideInInspector] public bool isDead;
        public SpriteRenderer sprite;
        public GameObject attackFX;


        [Header("Shield")]
        public bool hasShield;
        public bool canDamage;
        public GameObject dropPrisma;
        public GameObject shieldObj;
        private GameObject m_shieldObj;

        public static CharacterStats playerObj;

        public static bool cinzaTrue;
        public static bool vermelhoTrue;
        public static bool azulTrue;
        public static bool verdeTrue;


        CharacterAbilities charAbilities => GetComponent<CharacterAbilities>();
        MainCheckpoint mainCheckpoint => FindObjectOfType<MainCheckpoint>();
        CinemachineVirtualCamera cinemachineVirtualCamera => FindObjectOfType<CinemachineVirtualCamera>();

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
            cinemachineVirtualCamera.Follow = this.transform;
            cinemachineVirtualCamera.LookAt = this.transform;
        }

        void Update()
        {
            timeInvencible -= Time.deltaTime;

            if (timeInvencible <= 0)
            {
                canDamage = true;
            }

            if (health <= 0)
            {
                if (!isDead)
                {
                    mainCheckpoint.Death();
                    isDead = true;
                }
            }

            if (canDamage)
                sprite.color = Color.white;
            else
                sprite.color = Color.cyan;


        }

        public void DamageVoid(int dano)
        {
            if (canDamage)
            {
                if (hasShield)
                {
                    m_shieldObj.SetActive(false);
                    SpawnPrisma();
                    hasShield = false;
                }
                else
                {
                    health -= dano;
                }

                timeInvencible = m_timeInvencible;
                canDamage = false;
            }
        }

        public void GiveShield()
        {
            m_shieldObj.SetActive(true);
            hasShield = true;
        }

        public void SpawnPrisma()
        {
            GameObject prisma = Instantiate(dropPrisma, m_shieldObj.GetComponentInChildren<Transform>().position, Quaternion.identity);
            prisma.GetComponent<PrismaBase>().characterStats = gameObject.GetComponent<CharacterStats>();

        }
    }
}