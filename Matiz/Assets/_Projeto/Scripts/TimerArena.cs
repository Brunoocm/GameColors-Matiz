using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TimerArena : MonoBehaviour
{
    public float timer;
    public TextMeshProUGUI text;

    void Update()
    {
        timer -= Time.deltaTime;
        timer = Mathf.Round(timer * 100.0f) * 0.01f;

        text.text = "" + timer;

        //DateTime date = DateTime.Now;
    }
}
