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
        public Sprite Vida2;
        public Sprite Vida3;
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
            if (playerHealth == 0)
            {
                VidaImage[0].sprite = Vida0;
                VidaImage[1].sprite = Vida0;
                VidaImage[2].sprite = Vida0;
            }
            if (playerHealth == 1)
            {
                VidaImage[0].sprite = Vida1;
                VidaImage[1].sprite = Vida0;
                VidaImage[2].sprite = Vida0;
            }
            if (playerHealth == 2)
            {
                VidaImage[0].sprite = Vida1;
                VidaImage[1].sprite = Vida2;
                VidaImage[2].sprite = Vida0;
            }
            if (playerHealth == 3)
            {
                VidaImage[0].sprite = Vida1;
                VidaImage[1].sprite = Vida2;
                VidaImage[2].sprite = Vida3;
            }

            for (int i = 0; i < VidaImage.Length; i++)
            {
              
        
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
