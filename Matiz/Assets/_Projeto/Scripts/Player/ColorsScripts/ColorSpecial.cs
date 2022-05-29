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
        ProgressionManager progressionManager => FindObjectOfType<ProgressionManager>();

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
                progressionManager.cinza = true;
                progressionManager.vermelho = false;
                progressionManager.verde = false;
                progressionManager.azul = false;

                prisma.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;
            }
            else if (isVermelho) 
            {
                progressionManager.vermelho = true;
                progressionManager.cinza = false;
                progressionManager.verde = false;
                progressionManager.azul = false;

                prisma.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
            }
            else if (isVerde) 
            {
                progressionManager.verde = true;
                progressionManager.cinza = false;
                progressionManager.vermelho = false;
                progressionManager.azul = false;

                prisma.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 1, 0.24f);
            }
            else if (isAzul) 
            {
                progressionManager.azul = true;
                progressionManager.cinza = false;
                progressionManager.vermelho = false;
                progressionManager.verde = false;

                prisma.transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(0, 0.64f, 1);
            }
        }
    }
}