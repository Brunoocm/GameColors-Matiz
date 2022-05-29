using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TimerArena : MonoBehaviour
{
    public float timer;
    public float velocity;
    public float timePerEnemy;

    private float m_timer;
    private float reverseTimer;
    Slider slider => GetComponent<Slider>();
    Volume volume => GetComponentInChildren<Volume>();
    //public TextMeshProUGUI text;

    private void Start()
    {
        m_timer = timer;
        slider.maxValue = timer;
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
            volume.weight += Time.deltaTime * timer / 5000;
        }

    }

    public void AddTime()
    {
        timer += timePerEnemy;
        volume.weight -= timePerEnemy / 100;

        if (timer > m_timer)
        {
            timer = m_timer;
        }
    }
}
