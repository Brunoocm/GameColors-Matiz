using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OniricoStudios
{
    public class CharacterAbilities : MonoBehaviour
    {
        public bool cinzaTrue;
        public bool vermelhoTrue;
        public bool azulTrue;
        public bool verdeTrue;

        public CinzaAbility cinzaAbility;
        public VermelhoAbility vermelhoAbility;
        public AzulAbility azulAbility;
        public VerdeAbility verdeAbility;

        Slider AbilitiesCDSlider;
        CharacterStats charStats => GetComponent<CharacterStats>();

        private float time;

        private void Awake()
        {
            CharacterStats.cinzaTrue = true;
            CharacterStats.vermelhoTrue = false;
            CharacterStats.azulTrue = false;
            CharacterStats.verdeTrue = false;
        }
        void Start()
        {
            AbilitiesCDSlider = GameObject.Find("AbilitiesCD").GetComponentInChildren<Slider>();

            cinzaTrue = CharacterStats.cinzaTrue;
            vermelhoTrue = CharacterStats.vermelhoTrue;
            azulTrue = CharacterStats.azulTrue;
            verdeTrue = CharacterStats.verdeTrue;

            GameObject prisma = FindObjectOfType<PrismaMovement>().gameObject;
            GameObject prismaColor = prisma.transform.GetChild(0).gameObject;
            GameObject prismaTrail = prismaColor.transform.GetChild(0).gameObject;

            if (cinzaTrue)
            {
                prismaColor.GetComponent<SpriteRenderer>().color = Color.white;
                prismaTrail.GetComponent<TrailRenderer>().startColor = Color.white;
                prismaTrail.GetComponent<TrailRenderer>().endColor = Color.white;
            }
            else if (vermelhoTrue)
            {
                prismaColor.GetComponent<SpriteRenderer>().color = Color.red;
                prismaTrail.GetComponent<TrailRenderer>().startColor = Color.red;
                prismaTrail.GetComponent<TrailRenderer>().endColor = Color.red;
            }
            else if (verdeTrue)
            {
                prismaColor.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0.24f);
                prismaTrail.GetComponent<TrailRenderer>().startColor = new Color(0, 1, 0.24f);
                prismaTrail.GetComponent<TrailRenderer>().endColor = new Color(0, 1, 0.24f);
            }
            else if (azulTrue)
            {
                prismaColor.GetComponent<SpriteRenderer>().color = new Color(0, 0.64f, 1);
                prismaTrail.GetComponent<TrailRenderer>().startColor = new Color(0, 0.64f, 1);
                prismaTrail.GetComponent<TrailRenderer>().endColor = new Color(0, 0.64f, 1);
            }
        }

        void Update()
        {
            CharacterStats.cinzaTrue = cinzaTrue;
            CharacterStats.vermelhoTrue = vermelhoTrue;
            CharacterStats.azulTrue = azulTrue;
            CharacterStats.verdeTrue = verdeTrue;

            if (cinzaTrue)
            {
                cinzaAbility.Especial();
                cinzaAbility.Dash();

                AbilitiesCDSlider.value = cinzaAbility.m_cooldownSpecial;
                AbilitiesCDSlider.maxValue = cinzaAbility.cooldownSpecial;
            }
            else if (vermelhoTrue)
            {
                vermelhoAbility.Passiva();
                vermelhoAbility.Especial();
                vermelhoAbility.Dash();

                AbilitiesCDSlider.value = time;
                AbilitiesCDSlider.maxValue = vermelhoAbility.specialCooldown;
            }
            else if (azulTrue)
            {
                azulAbility.Passiva();
                azulAbility.Especial();
                azulAbility.Dash();
            }
            else if (verdeTrue)
            {
                verdeAbility.Passiva();
                verdeAbility.Especial();
                verdeAbility.Dash();
            }
        }

        public void spawnEspecialCinza()
        {
            if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, cinzaAbility.groundLayer)) // 6 = layermask ground
                {
                    GameObject obj = Instantiate(cinzaAbility.specialCinzaObj, new Vector3(hit.point.x, hit.point.y + 1, hit.point.z), Quaternion.identity);
                    obj.GetComponent<SpecialCinza>().forceKnockback = cinzaAbility.forceKnockback;
                    obj.GetComponent<SpecialCinza>().timeKnockback = cinzaAbility.timeKnockback;
                }

                cinzaAbility.m_cooldownSpecial = 0;

            }
        }

        public void SpecialRed()
        {

            if (time <= vermelhoAbility.specialCooldown)
            {

                time += Time.deltaTime;
            }
            else
            {

                if (Input.GetKeyDown(KeyCode.Mouse1))
                {

                    GameObject red = Instantiate(vermelhoAbility.specialVermelho, transform.position, Quaternion.identity);
                    red.transform.parent = transform;
                    time = 0;
                }
            }
        }


        [System.Serializable]
        public class CinzaAbility
        {
            public CharacterAbilities characterAbilities;

            [Header("Passiva")]
            public int numStacks;

            [Header("Especial")]
            public float timeKnockback;
            public float forceKnockback;
            [HideInInspector] public float cooldownSpecial;
            public float m_cooldownSpecial;
            public GameObject specialCinzaObj;
            public GameObject dashFX;
            public LayerMask groundLayer;


            [HideInInspector]
            public int currentStacks;

            public void PassivaTeste()
            {
                //HABILIDADE CINZA Script: EnemyHealth
            }

            public void Especial()
            {
                if (m_cooldownSpecial >= cooldownSpecial)
                {
                    characterAbilities.spawnEspecialCinza();
                }
                else
                {
                    m_cooldownSpecial += Time.deltaTime;
                }
            }

            public void SetSliderCinza()
            {
                
            }
            public void Dash()
            {

            }

        }
        [System.Serializable]
        public class VermelhoAbility
        {
            public int dashDamage, passiveDamage;
            public float dashMinForce, dashMaxForce, specialCooldown, passiveSpeed, dashSpeed;
            public GameObject specialVermelho;
            public GameObject dashFX;

            public CharacterStats characterStats;
            public CharacterMovement characterMove;
            public CharacterAbilities characterAbilities;

            public void Passiva()
            {
                //quanto menor for a ratio de vida, maior a velocidade e dano (puxar nos scripts de movimenta��o e stats)

                if (characterStats.health == 1)
                {
                    characterStats.damage = passiveDamage;
                    characterMove.speed = passiveSpeed;
                }
                else
                {
                    characterStats.damage = characterStats.m_damage;
                    characterMove.speed = characterMove.m_speed;
                }
            }

            public void Especial()
            {
                characterAbilities.SpecialRed();
            }

            public void Dash()
            {
                //
            }

        }
        [System.Serializable]
        public class AzulAbility
        {

            public void Passiva()
            {

            }

            public void Especial()
            {

            }

            public void Dash()
            {

            }

        }
        [System.Serializable]
        public class VerdeAbility
        {

            public void Passiva()
            {

            }

            public void Especial()
            {

            }

            public void Dash()
            {

            }
        }
    }
}