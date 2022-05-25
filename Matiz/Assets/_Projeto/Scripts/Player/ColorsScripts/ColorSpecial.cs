using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace OniricoStudios
{
    public class ColorSpecial : MonoBehaviour
    {
        public bool isSelected;
        public bool isCinza, isVermelho, isVerde, isAzul;

        CharacterAbilities characterAbilities;

        void Update()
        {
            if (isSelected)
            {
                SetColor();
                gameObject.transform.DOScale(1.5f, 0.3f);
            }
            else
            {
                gameObject.transform.DOScale(1, 0.3f);
            }
        }

        void SetColor()
        {
            characterAbilities = FindObjectOfType<CharacterAbilities>();
            GameObject prisma = FindObjectOfType<PrismaMovement>().gameObject;

            if (isCinza) 
            {
                characterAbilities.cinzaTrue = true; 
                characterAbilities.vermelhoTrue = false; 
                characterAbilities.verdeTrue = false; 
                characterAbilities.azulTrue = false;

                prisma.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
            }
            else if (isVermelho) 
            {
                characterAbilities.vermelhoTrue = true; 
                characterAbilities.cinzaTrue = false; 
                characterAbilities.verdeTrue = false; 
                characterAbilities.azulTrue = false;

                prisma.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
            }
            else if (isVerde) 
            {
                characterAbilities.verdeTrue = true; 
                characterAbilities.cinzaTrue = false; 
                characterAbilities.vermelhoTrue = false; 
                characterAbilities.azulTrue = false;

                prisma.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 1, 0.24f);
            }
            else if (isAzul) 
            {
                characterAbilities.azulTrue = true; 
                characterAbilities.cinzaTrue = false; 
                characterAbilities.vermelhoTrue = false; 
                characterAbilities.verdeTrue = false;

                prisma.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 0.64f, 1);
            }
        }
    }
}