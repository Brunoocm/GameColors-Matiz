using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace OniricoStudios
{
    public class Chroma : MonoBehaviour
    {
        public int index;
        public bool active;
        public LayerMask groundLayer;

        CharacterAbilities characterAbilities;
        SelectColorSpecial parent;

        private void Start()
        {
            parent = GetComponentInParent<SelectColorSpecial>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
                {
                    print("bruninho " + name);
                    SelectKit();
                }
            }
        }

        void SetColor()
        {
            characterAbilities = FindObjectOfType<CharacterAbilities>();

            switch (index)
            {
                case 0: //cinza

                    characterAbilities.cinzaTrue = true;
                    characterAbilities.vermelhoTrue = false;
                    characterAbilities.verdeTrue = false;
                    characterAbilities.azulTrue = false;

                    break;
                case 1: //vermelho

                    characterAbilities.cinzaTrue = false;
                    characterAbilities.vermelhoTrue = true;
                    characterAbilities.verdeTrue = false;
                    characterAbilities.azulTrue = false;

                    break;
                case 2: //verde

                    characterAbilities.cinzaTrue = false;
                    characterAbilities.vermelhoTrue = false;
                    characterAbilities.verdeTrue = true;
                    characterAbilities.azulTrue = false;

                    break;
                case 3: //azul

                    characterAbilities.cinzaTrue = false;
                    characterAbilities.vermelhoTrue = false;
                    characterAbilities.verdeTrue = false;
                    characterAbilities.azulTrue = true;

                    break;
            }
        }

        void SelectKit()
        {
            for (int i = 0; i < parent.transform.childCount; i++)
            {
                Chroma c = parent.transform.GetChild(i).GetComponent<Chroma>();

                if (c.index == index)
                {
                    c.active = true;
                    parent.transform.GetChild(i).transform.DOScale(1.5f, 0.3f);
                }
                else
                {
                    c.active = false;
                    parent.transform.GetChild(i).transform.DOScale(1, 0.3f);
                }

                SetColor();
            }
        }
    }
}