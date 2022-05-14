using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{

    [TextArea (3, 3)]
    public string[] sentences;
    [TextArea(3, 3)]
    public string[] sentencesBonus;
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

        //if (Input.GetKeyDown(KeyCode.M)) Restart();
    }

    public IEnumerator TextDisplayCoroutine(string[] sentence)
    {
        playingText = true;

        foreach (char letter in sentence[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(delay);
        }

        yield return new WaitForSeconds(delaySentences);
        NextSentence(sentence);


    }

    public void NextSentence(string[] sentence)
    {
        if(index < sentence.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(TextDisplayCoroutine(sentence));
        }
        else
        {
            playingText = false;
            textDisplay.text = "";
        }
    }

    public void Restart(string[] sentence)
    {
        index = 0;
        StartCoroutine(TextDisplayCoroutine(sentence));
    }
}
