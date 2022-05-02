using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OniricoStudios
{
    public class CharacterLifeUI : MonoBehaviour
    {
        //public GameObject[] imagemVida;

        //private CharacterStats characterStats;
        private int playerHealth;
        private int playerMaxHealth;


        public Image[] VidaImage;
        public Sprite Vida0;
        public Sprite Vida1;
        private void Start()
        {
            //characterStats = GameObject.FindObjectOfType<CharacterStats>().GetComponent<CharacterStats>();
        }

        private void Update()
        {
            playerHealth = CharacterStats.playerObj.health;

            //for (int i = 0; i <= imagemVida.Length; i++)
            //{
            //    if (playerHealth < i)
            //        imagemVida[i - 1].SetActive(false);
            //}

            for (int i = 0; i < VidaImage.Length; i++)
            {
                if (i < playerHealth)
                {
                    VidaImage[i].sprite = Vida1;
                }
                else
                {
                    VidaImage[i].sprite = Vida0;
                }
                if (i < 3)
                {
                    VidaImage[i].enabled = true;
                }
                else
                {
                    VidaImage[i].enabled = false;
                }
            }
        }
    }
}
