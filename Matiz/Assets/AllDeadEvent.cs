using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class AllDeadEvent : MonoBehaviour
{
    [Header("Events")]
    public UnityEvent m_MyEvent;


    Transform[] children;

    private int num;
    private bool desativar;
    void Start()
    {
        children = gameObject.GetComponentsInChildren<Transform>();

        if (children.Length <= 1)
        {
            desativar = true;
        }
    }

    void Update()
    {
        if (!desativar)
        {
            children = gameObject.GetComponentsInChildren<Transform>();

            if (children.Length <= 1)
            {
                m_MyEvent.Invoke();
                desativar = true;
            }
        }

    }
}