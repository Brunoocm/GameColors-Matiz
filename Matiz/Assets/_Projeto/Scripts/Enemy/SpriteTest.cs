using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTest : MonoBehaviour
{
    void Start()
    {
        transform.eulerAngles -= transform.parent.eulerAngles;
    }
}
