using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ColorSpecial : MonoBehaviour
{
    public bool isSelected;
    void Start()
    {
        
    }

    void Update()
    {
        if(isSelected)
        {
            gameObject.transform.DOScale(1.5f, 0.3f);
        }
        else
        {
            gameObject.transform.DOScale(1, 0.3f);
        }
    }
}
