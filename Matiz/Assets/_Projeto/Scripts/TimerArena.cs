using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerArena : MonoBehaviour
{
    public TextMeshProUGUI text;

    public float timer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timer = Mathf.Round(timer * 100.0f) * 0.01f;

        text.text = "" + timer;
    }
}
