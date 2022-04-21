using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    CharacterStats characterStats => GetComponent<CharacterStats>();

    void Start()
    {

    }

    void Update()
    {
        if (cinzaTrue)
        {
            spawnEspecialCinza();
            cinzaAbility.Especial();
            cinzaAbility.Dash();
        }
        else if (vermelhoTrue)
        {
            vermelhoAbility.Passiva();
            vermelhoAbility.Especial();
            vermelhoAbility.Dash();
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
        if (Input.GetKeyDown(KeyCode.Q))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, cinzaAbility.groundLayer)) // 6 = layermask ground
            {

            }
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 10000f))
            {
                GameObject obj = Instantiate(cinzaAbility.specialCinzaObj, hit.transform.position, Quaternion.identity);
                obj.GetComponent<SpecialCinza>().forceKnockback = cinzaAbility.forceKnockback;
                obj.GetComponent<SpecialCinza>().timeKnockback = cinzaAbility.timeKnockback;
            }
        }
    }

    [System.Serializable]
    public class CinzaAbility
    {
        [Header("Passiva")]
        public int numStacks;

        [Header("Especial")]
        public float timeKnockback;
        public float forceKnockback;
        public GameObject specialCinzaObj;
        public LayerMask groundLayer;


        [HideInInspector]
        public int currentStacks;

        public void PassivaTeste()
        {
            //HABILIDADE CINZA Script: EnemyHealth
        }

        public void Especial()
        {

        }

        public void Dash()
        {

        }

    }
    [System.Serializable]
    public class VermelhoAbility
    {
        public CharacterStats characterStats;

        public void Passiva()
        {
            float ratio = (float)characterStats.health / (float)characterStats.m_health;

            //quanto menor for o ratio, maior a velocidade e dano (puxar nos scripts de movimentação e stats)
        }

        public void Especial()
        {

        }

        public void Dash()
        {

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
