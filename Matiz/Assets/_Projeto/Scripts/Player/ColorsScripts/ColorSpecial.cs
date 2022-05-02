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
        void Start()
        {

        }

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

            if (isCinza) { characterAbilities.cinzaTrue = true; characterAbilities.vermelhoTrue = false; characterAbilities.verdeTrue = false; characterAbilities.azulTrue = false; }
            else if (isVermelho) { characterAbilities.vermelhoTrue = true; characterAbilities.cinzaTrue = false; characterAbilities.verdeTrue = false; characterAbilities.azulTrue = false; }
            else if (isVerde) { characterAbilities.verdeTrue = true; characterAbilities.cinzaTrue = false; characterAbilities.vermelhoTrue = false; characterAbilities.azulTrue = false; }
            else if (isAzul) { characterAbilities.azulTrue = true; characterAbilities.cinzaTrue = false; characterAbilities.vermelhoTrue = false; characterAbilities.verdeTrue = false; }

        }
    }
}