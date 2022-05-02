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
<<<<<<< Updated upstream

        public CinzaAbility cinzaAbility;
        public VermelhoAbility vermelhoAbility;
        public AzulAbility azulAbility;
        public VerdeAbility verdeAbility;

        CharacterStats characterStats => GetComponent<CharacterStats>();

        private float time;

=======

        public CinzaAbility cinzaAbility;
        public VermelhoAbility vermelhoAbility;
        public AzulAbility azulAbility;
        public VerdeAbility verdeAbility;

        CharacterStats characterStats => GetComponent<CharacterStats>();

        private float time;

>>>>>>> Stashed changes
        void Start()
        {

        }

<<<<<<< Updated upstream
<<<<<<< Updated upstream
            print("vermelho true");

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
=======
=======
>>>>>>> Stashed changes
        void Update()
        {
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

                if (time <= vermelhoAbility.specialCooldown)
<<<<<<< Updated upstream
                {
                    time += Time.deltaTime;
                }
                else
                {
=======
                {
                    time += Time.deltaTime;
                }
                else
                {
>>>>>>> Stashed changes
                    if (Input.GetKeyDown(KeyCode.Mouse1))
                    {
                        GameObject red = Instantiate(vermelhoAbility.specialVermelho, transform.position, Quaternion.identity);
                        red.transform.parent = transform;
                        time = 0;
                    }
                }
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
<<<<<<< Updated upstream
>>>>>>> Stashed changes
        }

        public void spawnEspecialCinza()
        {
            if (Input.GetMouseButtonDown(1))
            {
<<<<<<< Updated upstream
                GameObject obj = Instantiate(cinzaAbility.specialCinzaObj, new Vector3(hit.point.x, hit.point.y + 1, hit.point.z), Quaternion.identity);
                obj.GetComponent<SpecialCinza>().forceKnockback = cinzaAbility.forceKnockback;
                obj.GetComponent<SpecialCinza>().timeKnockback = cinzaAbility.timeKnockback;
            }
        }
    }

    public void SpecialRed()
    {
        print("special redding");

        if (time <= vermelhoAbility.specialCooldown)
        {
            print("1");

            time += Time.deltaTime;
=======
=======
        }

        public void spawnEspecialCinza()
        {
            if (Input.GetMouseButtonDown(1))
            {
>>>>>>> Stashed changes
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, cinzaAbility.groundLayer)) // 6 = layermask ground
                {
                    GameObject obj = Instantiate(cinzaAbility.specialCinzaObj, new Vector3(hit.point.x, hit.point.y + 1, hit.point.z), Quaternion.identity);
                    obj.GetComponent<SpecialCinza>().forceKnockback = cinzaAbility.forceKnockback;
                    obj.GetComponent<SpecialCinza>().timeKnockback = cinzaAbility.timeKnockback;

                }


            }
<<<<<<< Updated upstream
>>>>>>> Stashed changes
        }
        else
        {
            print("2");

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                print("clicking mouse 1");

                GameObject red = Instantiate(vermelhoAbility.specialVermelho, transform.position, Quaternion.identity);
                red.transform.parent = transform;
                time = 0;
            }
=======
>>>>>>> Stashed changes
        }
    }

<<<<<<< Updated upstream


        [System.Serializable]
        public class CinzaAbility
        {
            public CharacterAbilities characterAbilities;

            [Header("Passiva")]
            public int numStacks;

<<<<<<< Updated upstream
        [Header("Especial")]
        public float timeKnockback;
        public float forceKnockback;
        public GameObject specialCinzaObj;
        public GameObject dashFX;
        public LayerMask groundLayer;
=======
=======
        [System.Serializable]
        public class CinzaAbility
        {
            public CharacterAbilities characterAbilities;

            [Header("Passiva")]
            public int numStacks;

>>>>>>> Stashed changes
            [Header("Especial")]
            public float timeKnockback;
            public float forceKnockback;
            public GameObject specialCinzaObj;
            public LayerMask groundLayer;
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes


            [HideInInspector]
            public int currentStacks;

            public void PassivaTeste()
            {
                //HABILIDADE CINZA Script: EnemyHealth
            }

            public void Especial()
            {
                characterAbilities.spawnEspecialCinza();
            }

            public void Dash()
            {

            }

        }
        [System.Serializable]
        public class VermelhoAbility
        {
            public int dashDamage, passiveDamage;
            public float dashMinForce, dashMaxForce, specialCooldown, passiveSpeed;
            public GameObject specialVermelho;

<<<<<<< Updated upstream
<<<<<<< Updated upstream
    }
    [System.Serializable]
    public class VermelhoAbility
    {
        public int dashDamage, passiveDamage;
        public float dashMinForce, dashMaxForce, specialCooldown, passiveSpeed;
        public GameObject specialVermelho;
        public GameObject dashFX;

        public CharacterStats characterStats;
        public CharacterMovement characterMove;
        public CharacterAbilities characterAbilities;
=======
            public CharacterStats characterStats;
            public CharacterMovement characterMove;

            public void Passiva()
            {
                //quanto menor for a ratio de vida, maior a velocidade e dano (puxar nos scripts de movimentação e stats)
>>>>>>> Stashed changes

=======
            public CharacterStats characterStats;
            public CharacterMovement characterMove;

            public void Passiva()
            {
                //quanto menor for a ratio de vida, maior a velocidade e dano (puxar nos scripts de movimentação e stats)

>>>>>>> Stashed changes
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

            }

            public void Dash()
            {
                //
            }
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        }

        public void Especial()
        {
            characterAbilities.SpecialRed();
=======
=======
>>>>>>> Stashed changes

>>>>>>> Stashed changes
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