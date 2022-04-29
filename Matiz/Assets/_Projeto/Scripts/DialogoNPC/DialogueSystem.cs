using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{

    [TextArea (3, 3)]
    public string[] sentences;
    public float delaySentences;
    public float delay;

    [HideInInspector] public bool playingText;
    private int index;
    TextMeshProUGUI textDisplay => gameObject.GetComponentInChildren<TextMeshProUGUI>();
    Image background => gameObject.GetComponentInChildren<Image>();
    void Start()
    {
    }

    void Update()
    {
        background.rectTransform.sizeDelta = new Vector2(textDisplay.rectTransform.rect.width / 2.5f, textDisplay.rectTransform.rect.height /2);

        if (Input.GetKeyDown(KeyCode.M)) Restart();
    }

    public IEnumerator TextDisplayCoroutine()
    {
        playingText = true;

        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(delaySentences);
        NextSentence();


    }

    public void NextSentence()
    {
        if(index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(TextDisplayCoroutine());
        }
        else
        {
            playingText = false;
            textDisplay.text = "";
        }
    }

    public void Restart()
    {
        index = 0;
        StartCoroutine(TextDisplayCoroutine());
    }
}
