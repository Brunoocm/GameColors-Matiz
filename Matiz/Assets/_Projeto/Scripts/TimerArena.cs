using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace OniricoStudios
{
    public class TimerArena : MonoBehaviour
    {
        public float timer;
        public float velocity;
        public float timePerEnemy;
        public float timeEffect;

        private bool isFury;
        private bool isDebuffed;
        private float m_timer;
        private float reverseTimer;
        private float speed;
        private float attack;
        Slider slider => GetComponent<Slider>();
        Volume volume => GetComponentInChildren<Volume>();
        CharacterStats characterStats => FindObjectOfType<CharacterStats>();
        CharacterMovement characterMovement => FindObjectOfType<CharacterMovement>();

        //public TextMeshProUGUI text;

        private void Start()
        {
            m_timer = timer;
            slider.maxValue = timer;

            speed = characterMovement.speed;
        }
        void Update()
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime * velocity;
                timer = Mathf.Round(timer * 100.0f) * 0.01f;

                slider.value = timer;
            }
            if (timer <= m_timer)
            {
                //volume.weight += Time.deltaTime * timer / 5000;
                //volume.weight = (1.3f / timer) * 10;
            }
            
            if (timer <= 0)
            {
                if (!isDebuffed)
                {
                    StartCoroutine(Debuff());
                }
            }
            if (timer >= m_timer - 10)
            {
                if(!isFury)
                {
                    StartCoroutine(Furry());
                }
            }
        }

        public void AddTime()
        {
            timer += timePerEnemy;

            //volume.weight -= timePerEnemy / 100;

            if (timer > m_timer)
            {
                timer = m_timer;
            }
        }

        IEnumerator Furry()
        {
            isFury = true;

            characterMovement.speed = speed * 1.3f;

            yield return new WaitForSeconds(timeEffect);

            characterMovement.speed = speed;

            isFury = false;
        }
        IEnumerator Debuff()
        {
            isDebuffed = true;

            characterMovement.speed = speed / 2;

            yield return new WaitForSeconds(timeEffect);

            characterMovement.speed = speed;

            isDebuffed = false;
        }
    }
}
