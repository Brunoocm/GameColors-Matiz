using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace OniricoStudios
{
    public class CharacterInteract : MonoBehaviour
    {
        public GameObject intObj;


        //0.03f scale que tem que ficar no final

        void Appears()
        {
            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(intObj.transform.DOScale(0.04f, 0.1f)).Append(intObj.transform.DOScale(0.03f, 0.2f));
        }
        
        void Desappears()
        {
            intObj.transform.DOScale(0, 0.2f);
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.CompareTag("Harbor") || other.gameObject.CompareTag("Checkpoint"))
            {
                Appears();
            }
        } 
        private void OnTriggerExit(Collider other)
        {
            if(other.gameObject.CompareTag("Harbor") || other.gameObject.CompareTag("Checkpoint"))
            {
                Desappears();
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if(other.gameObject.CompareTag("Harbor") || other.gameObject.CompareTag("Checkpoint"))
            {
                if(Input.GetKeyDown(KeyCode.E))
                {
                    Desappears();
                }

            }
        }
    }
}
