using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace OniricoStudios
{
    public class CompleteDesafioUI : MonoBehaviour
    {
        public Sprite[] sprites;

        public GameObject pivot;

        public Image[] insignias;


        StartDesafio[] startDesafio;

        public static bool CompleteInsignia;

        void Start()
        {
            startDesafio = FindObjectsOfType<StartDesafio>();
        }

        void Update()
        {
            
        }

        public IEnumerator SetInsignia()
        {
            if (startDesafio[0].isFinished) { insignias[0].enabled = true; insignias[0].sprite = sprites[0]; }
            if (startDesafio[1].isFinished) { insignias[1].enabled = true; insignias[1].sprite = sprites[1]; }
            if (startDesafio[2].isFinished) { insignias[2].enabled = true; insignias[2].sprite = sprites[2]; }



            yield return new WaitForSeconds(1f);

            Sequence mySequence = DOTween.Sequence();
            mySequence.Append(pivot.transform.DOScale(4.5f, 0.3f))
                .Append(pivot.transform.DOScale(3, 0.3f));

            yield return new WaitForSeconds(3f);
            pivot.transform.DOScale(0, 0.3f);

        }
    }
}