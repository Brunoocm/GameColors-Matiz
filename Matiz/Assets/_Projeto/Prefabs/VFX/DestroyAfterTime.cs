using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniricoStudios
{
    public class DestroyAfterTime : MonoBehaviour
    {
        public float time;

        void Start()
        {
            Destroy(gameObject, time);
        }
    }
}
