using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            cinzaTrue = CharacterStats.cinzaTrue;
            vermelhoTrue = CharacterStats.vermelhoTrue;
            azulTrue = CharacterStats.azulTrue;
            verdeTrue = CharacterStats.verdeTrue;
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
            }
            else if (vermelhoTrue)
            {
                vermelhoAbility.Passiva();
                vermelhoAbility.Especial();
                vermelhoAbility.Dash();


                //if(time <= vermelhoAbility.specialCooldown)
                //{
                //    time += Time.deltaTime;
                //}
                //else
                //{
                //    if (Input.GetKeyDown(KeyCode.Mouse1))
                //    {
                //        GameObject red = Instantiate(vermelhoAbility.specialVermelho, transform.position, Quaternion.identity);
                //        red.transform.parent = transform;
                //        time = 0;
                //    }
                //}
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

                cinzaAbility.m_cooldownSpecial = cinzaAbility.cooldownSpecial;

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
                if (m_cooldownSpecial <= 0)
                {
                    characterAbilities.spawnEspecialCinza();
                }
                else
                {
                    m_cooldownSpecial -= Time.deltaTime;
                }
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