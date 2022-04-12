using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterLifeUI : MonoBehaviour
{
    public GameObject[] imagemVida;

    private CharacterStats characterStats;
    private int playerHealth;
    private int playerMaxHealth;

    private void Start()
    {
        characterStats = GameObject.FindObjectOfType<CharacterStats>( ).GetComponent<CharacterStats>();
    }

    private void Update()
    {
        playerHealth = characterStats.health;

        for(int i = 0; i <= imagemVida.Length; i++)
        {
            if ( playerHealth < i )
                imagemVida[ i - 1 ].SetActive( false );
        }
    }
}
