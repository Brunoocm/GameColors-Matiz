using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace OniricoStudios
{
    public class ExplosionBullet : MonoBehaviour
    {
        public float timeToDisappear;

        void Start()
        {
            Destroy(gameObject, timeToDisappear);
        }
    }
}